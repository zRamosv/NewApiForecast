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
    public class ProveedoresController : ControllerBase
    {
        private readonly ForecastContext _context;
        public ProveedoresController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Proveedores.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProveedoresInsert proveedor)
        {
            var insert = new Proveedores
            {
                Nombre = proveedor.Nombre,
                Contacto = proveedor.Contacto,
            };
            _context.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProveedor), new { id = insert.Provider_id }, insert);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProveedoresDTO proveedor)
        {
            var update = await _context.Proveedores.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            update.Contacto = proveedor.Contacto ?? update.Contacto;
            update.Nombre = proveedor.Nombre ?? update.Nombre;
            await _context.SaveChangesAsync();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.Proveedores.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Proveedores.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
