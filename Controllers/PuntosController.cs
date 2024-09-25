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
    public class PuntosController : ControllerBase
    {
        private readonly ForecastContext _context;
        public PuntosController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Puntos.Include(x => x.Sucursales).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPunto(int id)
        {
            var punto = await _context.Puntos.Include(x => x.Sucursales).FirstOrDefaultAsync(x => x.Points_id == id);
            if (punto == null)
            {
                return NotFound();
            }
            return Ok(punto);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PuntosInsert punto)
        {
            var insert = new Puntos
            {
                PesosPorPunto = punto.PesosPorPunto,
                PuntosMinimos = punto.PuntosMinimos,
                Sucursal_id = punto.Sucursal_id
            };
            _context.Puntos.Add(insert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPunto), new { id = punto }, punto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PuntosDTO punto)
        {
            var update = await _context.Puntos.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            update.PesosPorPunto = punto.PesosPorPunto ?? update.PesosPorPunto;
            update.PuntosMinimos = punto.PuntosMinimos ?? update.PuntosMinimos;
            update.Sucursal_id = punto.Sucursal_id ?? update.Sucursal_id;
            await _context.SaveChangesAsync();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.Puntos.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Puntos.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
