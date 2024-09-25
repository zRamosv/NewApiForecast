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
    public class RolesController : ControllerBase
    {
        private readonly ForecastContext _context;

        public RolesController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Roles.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRol(int id)
        {
            var rol = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (rol == null){
                return NotFound();
            }
            return Ok(rol);
        }
        [HttpPost]
        public async Task<IActionResult> Post(RolesInsert rol)
        {
            var insert = new Roles
            {
                Nombre = rol.Name
            };
            _context.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRol), new { id = insert.Id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RolesDTO rol)
        {
            var update = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (update == null)
            {
                return NotFound();
            }
            update.Nombre = rol.Name ?? update.Nombre;
            await _context.SaveChangesAsync();
            return Ok(update);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}