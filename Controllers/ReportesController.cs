using ApiForecast.Data;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using ApiForecast.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _service;

        public ReportesController(IReportesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetReportes());

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReporte(int id)
        {
            var reporte = await _service.GetReporte(id);
            return reporte is null ? NotFound() : Ok(reporte);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ReportesInsert reporte)
        {
            var newReporte = await _service.CreateReporte(reporte);
            return Ok(reporte);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Reportes eliminado = await _service.DeleteReporte(id);
            return eliminado is null ? NotFound() : NoContent();
        }

    }
}