using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ImpresorasController : ControllerBase
    {
        private readonly ForecastContext _context;

        public ImpresorasController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetImpresoras()
        {
            return Ok(await _context.Impresoras.Include(x => x.Sucursales).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImpresora(int id)
        {
            var impresora = await _context.Impresoras.Include(x => x.Sucursales).FirstOrDefaultAsync(x => x.Printer_id == id);
            if (impresora == null)
            {
                return NotFound();
            }

            return Ok(impresora);
        }
        [HttpPost]
        public async Task<IActionResult> PostImpresoras(ImpresorasInsert impresora)
        {
            var insert = new Impresoras
            {
                TipoDocumento = impresora.TipoDocumento,
                Impresora = impresora.Impresora,
                Sucursal_id = impresora.Sucursal_id
            };
            _context.Impresoras.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetImpresora), new { id = insert.Printer_id }, insert);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImpresoras(int id, ImpresorasDTO impresora)
        {
            var update = await _context.Impresoras.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = impresora.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(impresora);
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
        public async Task<IActionResult> DeleteImpresoras(int id)
        {
            var delete = await _context.Impresoras.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Impresoras.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}