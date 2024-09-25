using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalesController : ControllerBase
    {
        private readonly ForecastContext _context;

        public SucursalesController(ForecastContext context)
        {
            _context = context;
        }

        [HttpPost("CrearSucursal(ClonarBD)")]
        public async Task<IActionResult> CrearSucursal(string nombre)
        {


            var nombreDeDB = new SqlParameter("@dbname", nombre);
            await _context.Database.ExecuteSqlRawAsync("EXEC CloneDatabaseWithoutData 'dbforecast', @dbname;", nombreDeDB);
            return Ok("Base de datos nueva creada para la sucursal " + nombre);


        }
        [HttpPost]
        public async Task<IActionResult> CrearSucursal(SucursalesInsert sucursal)
        {
            var insert = new Sucursales
            {
                Nombre = sucursal.Nombre,
                Direccion = sucursal.Direccion

            };

            _context.Sucursales.Add(insert);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSucursal), new { id = insert.Sucursal_id }, insert);

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursales>>> GetSucursales()
        {
            return await _context.Sucursales.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursales>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return sucursal;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSucursal(int id, SucursalesDTO sucursal)
        {
            var update = await _context.Sucursales.FirstOrDefaultAsync(x => x.Sucursal_id == id);
            if (update == null)
            {
                return NotFound();
            }
            update.Nombre = sucursal.Nombre ?? update.Nombre;
            update.Direccion = sucursal.Direccion ?? update.Direccion;
            _context.Entry(sucursal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var delete = await _context.Sucursales.FirstOrDefaultAsync(x => x.Sucursal_id == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Sucursales.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}