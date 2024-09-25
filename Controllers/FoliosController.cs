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
    public class FoliosController : ControllerBase
    {
        private readonly ForecastContext _context;
        public FoliosController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFolios()
        {
            return Ok(await _context.Folios.Include(x => x.Sucursales).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolio(int id)
        {
            var folio = await _context.Folios.Include(x => x.Sucursales).FirstOrDefaultAsync(x => x.Folio_id == id);
            if (folio == null)
            {
                return NotFound();
            }
            return Ok(folio);
        }

        [HttpPost]
        public async Task<IActionResult> PostFolios([FromBody] FoliosInsert folio){
            var insert = new Folios{
                TipoDocumento = folio.TipoDocumento,
                SiguienteFolio = folio.SiguienteFolio,
                Sucursal_id = folio.Sucursal_id
            };
            _context.Folios.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFolio), new { id = insert.Folio_id }, insert);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolios(int id, [FromBody] FoliosDTO folio)
        {
            var update = await _context.Folios.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = folio.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(folio);
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
        public async Task<IActionResult> DeleteFolios(int id)
        {
            var delete = await _context.Folios.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Folios.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
