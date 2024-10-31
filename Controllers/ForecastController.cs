using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.GetModels;
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
            var compras = await _context.Compras.AsNoTracking().Include(x => x.Product).Where(x => x.Product.Group_Id == forecast.Group_id).ToListAsync();
            if (compras.Count == 0)
            {
                return BadRequest("EL producto no tiene movimientos registrados para generar el forecast");
            }
            var forecastExistentes = await _context.Forecast.AsNoTracking().Where(x => x.Nombre_ejercicio == forecast.Nombre_Ejercicio).ToListAsync();
            if (forecastExistentes.Count > 0)
            {
                return BadRequest("Ya existe un forecast con el mismo nombre");
            }
            var groupId = new SqlParameter("@group_id", forecast.Group_id);
            var meses = new SqlParameter("@meses_forecast", forecast.Meses_Forecast);
            var nombre = new SqlParameter("@nombre_ejercicio", forecast.Nombre_Ejercicio);

            await _context.Database.ExecuteSqlRawAsync("EXEC GenerarForecastRound @group_id, @meses_forecast, @nombre_ejercicio;", groupId, meses, nombre);

            var forecastCreado = await _context.Forecast.Include(x => x.Producto).ThenInclude(x => x.Grupos).OrderByDescending(x => x.Id_forecast).FirstOrDefaultAsync();
            var output = new GetForecast
            {
                Id_forecast = forecastCreado.Id_forecast,
                Group_id = forecastCreado.Producto.Group_Id,
                Group = forecastCreado.Producto.Grupos,
                Meses = forecastCreado.Meses,
                Anio = forecastCreado.Anio,
                Cantidad_estimado = forecastCreado.Cantidad_estimado,
                Fecha_generacion = forecastCreado.Fecha_generacion,
                Nombre_ejercicio = forecastCreado.Nombre_ejercicio,
                Recomendaciones = forecastCreado.Recomendaciones
            };

            return Ok(output);
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
            var forecasts = await _context.Forecast.Include(x => x.Producto).ThenInclude(x => x.Grupos).ToListAsync();
            var output = forecasts.Select(x => new GetForecast
            {
                Id_forecast = x.Id_forecast,
                Group_id = x.Producto.Group_Id,
                Group = x.Producto.Grupos,
                Meses = x.Meses,
                Anio = x.Anio,
                Cantidad_estimado = x.Cantidad_estimado,
                Fecha_generacion = x.Fecha_generacion,
                Nombre_ejercicio = x.Nombre_ejercicio,
                Recomendaciones = x.Recomendaciones

            }).ToList();
            return Ok(output);
        }


        [HttpGet("GetForecast/{id}")]
        public async Task<IActionResult> GetForecast(int id)
        {
            var forecast = await _context.Forecast.Include(x => x.Producto).ThenInclude(x => x.Grupos).FirstOrDefaultAsync(x => x.Id_forecast == id);
            if (forecast == null)
            {
                return NotFound();
            }
            var output = new GetForecast
            {
                Id_forecast = forecast.Id_forecast,
                Group_id = forecast.Producto.Group_Id,
                Group = forecast.Producto.Grupos,
                Meses = forecast.Meses,
                Anio = forecast.Anio,
                Cantidad_estimado = forecast.Cantidad_estimado,
                Fecha_generacion = forecast.Fecha_generacion,
                Nombre_ejercicio = forecast.Nombre_ejercicio,
                Recomendaciones = forecast.Recomendaciones
            };
            return Ok(output);
        }
        [HttpGet("GetDetalleForecast")]
        public async Task<IActionResult> GetDetalleForecast([FromQuery]string nombreEjercico)
        {
            var detalleForecast = await _context.DetalleForecast.AsNoTracking().Include(x => x.Producto).ThenInclude(x => x.Grupos).Include(x=>x.Producto).ThenInclude(x=>x.Proveedor).Where(x => x.Nombre_Ejercicio == nombreEjercico).ToListAsync();
            return Ok(detalleForecast);
        }
        [HttpGet("GetAllDetallesForecast")]
        public async Task<IActionResult> GetAllDetallesForecast()
        {
            var detalleForecast = await _context.DetalleForecast.AsNoTracking().Include(x => x.Producto).ThenInclude(x => x.Grupos).Include(x=>x.Producto).ThenInclude(x=>x.Proveedor).ToListAsync();
            return Ok(detalleForecast);
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