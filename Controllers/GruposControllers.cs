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
    public class GruposController : ControllerBase
    {
        private readonly ForecastContext _context;

        public GruposController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrupos()
        {
            return Ok(await _context.Grupos.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrupo(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            return Ok(grupo);
        }

        [HttpPost]
        public async Task<IActionResult> PostGrupos(GruposInsert grupo)
        {
            var insert = new Grupos{
                Clave = grupo.Clave,
            };
            _context.Grupos.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGrupo), new { id = insert.Group_id }, grupo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrupos(int id, GruposDTO grupo)
        {
            var update = await _context.Grupos.FindAsync(id);
            if (update == null){
                return NotFound();
            }
            update.Clave = grupo.Clave ?? update.Clave;
            
            await _context.SaveChangesAsync();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupos(int id)
        {
            var delete = await _context.Grupos.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Grupos.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}