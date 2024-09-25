using ApiForecast.Data;
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

        [HttpPost]
        public async Task<IActionResult> CrearSucursal(string nombre)
        {

            var nombreDeDB = new SqlParameter("@dbname", nombre);
            await _context.Database.ExecuteSqlRawAsync("EXEC CloneDatabaseWithoutData 'dbforecast', @dbname;", nombreDeDB);
            return Ok("Base de datos nueva creada para la sucursal " + nombre);


        }
    }
}