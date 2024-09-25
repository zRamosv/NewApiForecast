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
    public class PermisosController : ControllerBase
    {

        private readonly ForecastContext _context;
        public PermisosController(ForecastContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Permisos.Include(x => x.User).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermiso(int id)
        {
            var permiso = await _context.Permisos.Include(x => x.User).FirstOrDefaultAsync(x => x.Permiso_id == id);
            if (permiso == null)
            {
                return NotFound();
            }
            return Ok(permiso);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PermisosInsert permiso)
        {
            var insert = new Permisos
            {
                User_id = permiso.User_id,
                Modulo = permiso.Modulo,
                Nivel_acceso = permiso.Nivel_acceso
            };
            _context.Permisos.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPermiso), new { id = insert.Permiso_id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PermisosDTO permiso)
        {
            var update = await _context.Permisos.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = permiso.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(permiso);
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
            var delete = await _context.Permisos.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Permisos.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}