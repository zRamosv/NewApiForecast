using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("[controller]")]
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
            return Ok(await _context.Vendedores.FirstOrDefaultAsync(x => x.Vendor_id == id));
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

    }
}