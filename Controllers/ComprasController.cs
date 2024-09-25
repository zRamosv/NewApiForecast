using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.InsertModels;
using ApiForescast.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : Controller
    {
        private readonly ForecastContext _context;
        public ComprasController(ForecastContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetCompras()
        {
            return Ok(await _context.Compras.Include(x => x.Product).Include(x => x.Proveedor).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompra(int id)
        {
            var compra = await _context.Compras.Include(x => x.Product).Include(x => x.Proveedor).FirstOrDefaultAsync(x => x.Purchase_id == id);
            if (compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }
        [HttpPost]
        public async Task<IActionResult> PostCompras(ComprasInsert compra)
        {
            var insert = new Compras
            {
                Product_id = compra.Product_id,
                Fecha = compra.Fecha,
                Cantidad = compra.Cantidad,
                Precio = compra.Precio,
                Provider_id = compra.Provider_id,
                MonedaUSD = compra.MoendaUSD
            };
            _context.Compras.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCompra), new { id = insert.Purchase_id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompras(int id, ComprasDTO compra)
        {
            var update = await _context.Compras.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = compra.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(compra);
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
        public async Task<IActionResult> DeleteCompras(int id)
        {
            var delete = await _context.Compras.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Compras.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}