using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Http.Features;
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

        [HttpGet("{idUser}")]
        public async Task<IActionResult> GetPermiso(int idUser)
        {
            var permiso = await _context.Permisos.Include(x => x.User).Where(x => x.User_id == idUser).ToListAsync();
            if (permiso == null)
            {
                return NotFound();
            }
            return Ok(permiso);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PermisosInsert permiso)
        {
            var permisosUser = await _context.Permisos.Where(x => x.User_id == permiso.User_id).ToListAsync();
            if (permisosUser != null){
                _context.Permisos.RemoveRange(permisosUser);
                await _context.SaveChangesAsync();
            }
            foreach (var item in permiso.Permisos){
                var insert = new Permisos{
                     User_id = permiso.User_id,
                     Modulo = item.Modulo,
                     Aplicacion = item.Aplicacion,
                     Nivel_acceso = item.Nivel_acceso

                };
                await _context.Permisos.AddAsync(insert);
            }
            await _context.SaveChangesAsync();
            return Ok(permiso);
            
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