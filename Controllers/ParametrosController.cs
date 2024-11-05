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
    public class ParametrosController : ControllerBase
    {
        private readonly ForecastContext _context;
        public ParametrosController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Parametros.Include(x => x.Sucursal).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParametro(int sucursalId)
        {
            var parametro = await _context.Parametros
                .Include(x => x.Sucursal)
                .FirstOrDefaultAsync(x => x.Sucursal_id == sucursalId);
                
            if (parametro == null)
            {
                return NotFound();
            }
            return Ok(parametro);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ParametrosInsert parametro)
        {
            var insert = new Models.Entities.Parametros
            {
                Sucursal_id = parametro.Sucursal_id,
                FirmaSupervisor = parametro.FirmaSupervisor,
                ExistenciasRequeridas = parametro.ExistenciasRequeridas,
            };

            _context.Parametros.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParametro), new { id = insert.Parameters_id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ParametrosDTO parametro)
        {
            var update = await _context.Parametros.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = parametro.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(parametro);
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
            var delete = await _context.Parametros.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Parametros.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}