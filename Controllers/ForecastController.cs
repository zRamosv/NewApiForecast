using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly ForecastContext _context;
        public ForecastController(ForecastContext context)
        {
            _context = context;
        }
        [HttpPost("GenerarForecast")]
        public async Task<IActionResult> ForecastGenerate(ForecastDTO forecast)
        {
            var groupId = new SqlParameter("@group_id", forecast.Group_id);
            var meses = new SqlParameter("@meses_forecast", forecast.Meses_Forecast);
            var nombre = new SqlParameter("@nombre_ejercicio", forecast.Nombre_Ejercicio);

            await _context.Database.ExecuteSqlRawAsync("EXEC GenerarForecast @group_id, @meses_forecast, @nombre_ejercicio;", groupId, meses, nombre);

            var forecastCreado = await _context.Forecast.OrderByDescending(x => x.Id_forecast).FirstOrDefaultAsync();

            return Ok(forecastCreado);
        }
        [HttpGet("GetParametros")]
        public async Task<IActionResult> GetParametros()
        {
            return Ok(await _context.ParametrosConfiguracion.ToListAsync());
        }
        [HttpGet("GetParametros/{id}")]
        public async Task<IActionResult> GetParametros(int id)
        {
            var parametro = await _context.ParametrosConfiguracion.FirstOrDefaultAsync(x => x.Id_Parametro == id);
            if (parametro == null)
            {
                return NotFound();
            }
            return Ok(parametro);
        }
        [HttpPut("CambiarParametros/{id}")]
        public async Task<IActionResult> CambiarParametros(int id, EditarParametroCofiguracion param)
        {
            var update = await _context.ParametrosConfiguracion.FirstOrDefaultAsync(x => x.Id_Parametro == id);
            if (update == null)
            {
                return NotFound();
            }
            update.Valor = param.Valor;
            await _context.SaveChangesAsync();
            return Ok(update);
        }
        [HttpGet("GetAllForecasts")]
        public async Task<IActionResult> GetAllForecasts()
        {
            return Ok(await _context.Forecast.ToListAsync());
        }

        [HttpGet("GetForecast/{id}")]
        public async Task<IActionResult> GetForecast(int id)
        {
            var forecast = await _context.Forecast.FirstOrDefaultAsync(x => x.Id_forecast == id);
            if (forecast == null)
            {
                return NotFound();
            }
            return Ok(forecast);
        }
        [HttpDelete("DeleteForecast/{id}")]
        public async Task<IActionResult> DeleteForecast(int id)
        {
            var delete = await _context.Forecast.FirstOrDefaultAsync(x => x.Id_forecast == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Forecast.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}