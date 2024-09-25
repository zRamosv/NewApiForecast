using ApiForecast.Data;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ForecastContext _context;
        public ReportesController(ForecastContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Reportes.ToListAsync());

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReporte(int id)
        {
            var reporte = await _context.Reportes.FirstOrDefaultAsync(x => x.Report_id == id);
            if (reporte == null){
                return NotFound();
            }
            return Ok(reporte);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ReportesInsert reporte)
        {
            var insert = new Reportes
            {
                Tipo = reporte.Tipo,
                Fecha_inicio = DateOnly.FromDateTime(reporte.Fecha_inicio),
                Fecha_fin = DateOnly.FromDateTime(reporte.Fecha_fin),
                Detalles = reporte.Detalles

            };
            _context.Reportes.Add(insert);
            await _context.SaveChangesAsync();
            return Ok(reporte);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.Reportes.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Reportes.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}