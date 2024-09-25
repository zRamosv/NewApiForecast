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
    public class VendedoresController : ControllerBase
    {
        private readonly ForecastContext _context;
        public VendedoresController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Vendedores.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendedor(int id)
        {
            var vendedores = await _context.Vendedores.FirstOrDefaultAsync(x => x.Vendor_id == id);
            if (vendedores == null)
            {
                return NotFound();
            }
            return Ok(vendedores);
        }
        [HttpPost]
        public async Task<IActionResult> Post(VendedoresInsert vendedores)
        {
            var insert = new Vendedores{
                Nombre = vendedores.Nombre,
                Comision = vendedores.Comision
            };
            _context.Vendedores.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVendedor), new { id = insert.Vendor_id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VendedoresDTO vendedores)
        {
            var update = await _context.Vendedores.FirstOrDefaultAsync(x => x.Vendor_id == id);
            if (update == null)
            {
                return NotFound();
            }
            update.Nombre = vendedores.Nombre ?? update.Nombre;
            update.Comision = vendedores.Comision ?? update.Comision;
            await _context.SaveChangesAsync();
            return Ok(update);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.Vendedores.FirstOrDefaultAsync(x => x.Vendor_id == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Vendedores.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}