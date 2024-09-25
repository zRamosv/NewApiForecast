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
    public class VentasController : ControllerBase
    {
        private readonly ForecastContext _context;

        public VentasController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Ventas.Include(x => x.Cliente).Include(x => x.Productos).Include(x => x.Vendedores).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenta(int id)
        {
            return Ok(await _context.Ventas.Include(x => x.Cliente).Include(x => x.Productos).Include(x => x.Vendedores).FirstOrDefaultAsync(x => x.Venta_Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(VentasInsert venta)
        {
            var insert = new Ventas
            {
                Product_Id = venta.Product_Id,
                Fecha = venta.Fecha,
                Cantidad = venta.Cantidad,
                Precio = venta.Precio,
                Client_id = venta.Client_id,
                MonedaUSD = venta.MonedaUSD,
                Vendor_Id = venta.Vendor_Id
            };

            _context.Ventas.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVenta), new { id = insert.Venta_Id }, insert);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VentasDTO venta)
        {
            var update = await _context.Ventas.FirstOrDefaultAsync(x => x.Venta_Id == id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = venta.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(venta);
                    if (newValue != null)
                    {
                        property.SetValue(update, newValue);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(update);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.Ventas.FirstOrDefaultAsync(x => x.Venta_Id == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Ventas.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}