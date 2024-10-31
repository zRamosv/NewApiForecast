using System.Text.RegularExpressions;
using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrdenDeCompraController : ControllerBase
    {
        private readonly ForecastContext _context;
        public OrdenDeCompraController(ForecastContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrden(InsertOrdenCompra orden)
        {
            var insertOrden = new OrdenesDeCompra
            {
                Fecha_solicitud = GetCdmxTime(),
                Estado = "Generado"
            };
            await _context.OrdenesDeCompra.AddAsync(insertOrden);
            await _context.SaveChangesAsync();
            foreach (var item in orden.OrdenDeCompra)
            {
                var insertDetalle = new DetallesOrdenCompra
                {
                    Id_orden = insertOrden.Id_orden,
                    Id_producto = item.Id_Producto,
                    Cantidad = item.Cantidad,
                    Fecha_embarque = null,
                    Fecha_aduana = null,
                    Fecha_llegada_destino = null,
                    Estado = "Generado"
                };
                await _context.DetallesOrdenCompra.AddAsync(insertDetalle);
            }
            await _context.SaveChangesAsync();
            return Ok(orden);

        }
        [HttpPost("GenerarOrdenDeCompraConForecastDetalles/{idDetalleForecast}")]
        public async Task<IActionResult> GenerarOrdenDeCompraConForecastDetalles(int idDetalleForecast)
        {
            var detalleForecast = await _context.DetalleForecast.FindAsync(idDetalleForecast);
            var match = Regex.Match(detalleForecast.Recomendaciones, @"(\d+\.\d+)\s+unidades\s+para\s+cubrir");

            if (!match.Success)
            {
                return BadRequest();
            }
            var unidades = (int)Math.Round(double.Parse(match.Groups[1].Value));
            var insertOrden = new OrdenesDeCompra
            {
                Fecha_solicitud = DateTime.Now,
                Estado = "Generado",

            };
            await _context.OrdenesDeCompra.AddAsync(insertOrden);
            await _context.SaveChangesAsync();

            var insertDetalle = new DetallesOrdenCompra
            {
                Id_orden = insertOrden.Id_orden,
                Id_producto = detalleForecast.Id_Producto,
                Cantidad = unidades,
                Fecha_embarque = null,
                Fecha_aduana = null,
                Fecha_llegada_destino = null,
                Estado = "Generado"
            };
            await _context.DetallesOrdenCompra.AddAsync(insertDetalle);

            await _context.SaveChangesAsync();
            return Ok(insertDetalle);
        }
        [HttpPost("CambiarEstadoAEmbarcado/{idDetalleOrden}")]
        public async Task<IActionResult> CambiarEstadoAEmbarcado(int idDetalleOrden){
            var detalle = await _context.DetallesOrdenCompra.FindAsync(idDetalleOrden);
            if(detalle == null){
                return NotFound();
            }
            detalle.Estado = "Embarcado";
            detalle.Fecha_embarque = GetCdmxTime();
            
            await _context.SaveChangesAsync();
            return Ok(detalle);
        }
        [HttpPost("CambiarEstadoAEnAduana/{idDetalleOrden}")]
        public async Task<IActionResult> CambiarEstadoAEnAduana(int idDetalleOrden){
            var detalle = await _context.DetallesOrdenCompra.FindAsync(idDetalleOrden);
            if(detalle == null){
                return NotFound();
            }
            detalle.Estado = "En Aduana";
            detalle.Fecha_aduana = GetCdmxTime();
            await _context.SaveChangesAsync();
            return Ok(detalle);
        }
        [HttpPost("CambiarEstadoAEntregado/{idDetalleOrden}")]
        public async Task<IActionResult> CambiarEstadoAEntregado(int idDetalleOrden){
            var detalle = await _context.DetallesOrdenCompra.FindAsync(idDetalleOrden);
            if(detalle == null){
                return NotFound();
            }
            detalle.Estado = "Entregado";
            detalle.Fecha_llegada_destino = GetCdmxTime();
            await _context.SaveChangesAsync();
            return Ok(detalle);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrdenes()
        {


            return Ok(await _context.OrdenesDeCompra.AsNoTracking().ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrden(int id)
        {
            return (await _context.OrdenesDeCompra.FindAsync(id)) != null ? Ok(await _context.OrdenesDeCompra.FindAsync(id)) : NotFound();
        }
        [HttpGet("DetalleOrdenDeCompra/{id}")]
        public async Task<IActionResult> GetDetalleOrden(int id)
        {
            var orden = await _context.DetallesOrdenCompra.AsNoTracking().Include(x => x.Productos).Include(x => x.OrdenDeCompra).Include(x => x.Productos).ThenInclude(x => x.Proveedor).Include(x => x.Productos).ThenInclude(x => x.Grupos).Where(x => x.Id_orden == id).ToListAsync();
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.OrdenesDeCompra.FirstOrDefaultAsync(x => x.Id_orden == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.OrdenesDeCompra.Remove(delete);
            await _context.SaveChangesAsync();
            return Ok(delete);

        }
        [HttpPut("EditarDetalleCompra/{id}")]
        public async Task<IActionResult> Put(int id, DetalleCompraDTO orden)
        {
            var update = await _context.DetallesOrdenCompra.FirstOrDefaultAsync(x => x.Id_detalle == id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = orden.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(orden);
                    if (newValue != null)
                    {
                        property.SetValue(update, newValue);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return Ok(update);
        }

        private DateTime GetCdmxTime(){
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo cdmxTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return  TimeZoneInfo.ConvertTimeFromUtc(utcNow, cdmxTimeZone);
        }
    }
}