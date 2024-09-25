using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoSucursalesController : ControllerBase
    {

        private readonly ForecastContext _context;

        public AccesoSucursalesController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccesoSucursales()
        {

            return Ok(await _context.AccesoSucursales.Include(x => x.Usuario).Include(x => x.Sucursal).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccesoSucursal(int id)
        {
            var accesoSucursal = await _context.AccesoSucursales.Include(x => x.Usuario).Include(x => x.Sucursal).FirstOrDefaultAsync(x => x.User_id == id);
            if (accesoSucursal == null)
            {
                return NotFound();
            }

            return Ok(accesoSucursal);
        }
        [HttpPost]
        public async Task<IActionResult> PostAccesoSucursal(AccesoSucursalesInsert accesoSucursal)
        {
            var insert = new AccessoSucursales
            {
                User_id = accesoSucursal.User_id,
                Nombre_user = accesoSucursal.Nombre_user,
                Sucursal_id = accesoSucursal.Sucursal_id
            };

            _context.AccesoSucursales.Add(insert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccesoSucursal), new { id = insert.User_id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccesoSucursal(int id, AccesoSucursalesDTO accesoSucursal)
        {

            var update = await _context.AccesoSucursales.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {


                var dtoProperty = accesoSucursal.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(accesoSucursal);
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
        public async Task<IActionResult> DeleteAccesoSucursal(int id)
        {
            var delete = await _context.AccesoSucursales.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.AccesoSucursales.Remove(delete);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}