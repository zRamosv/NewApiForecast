using ApiForecast.Data;
using ApiForecast.Models.ReportesModels.ReportesProductos;
using ApiForecast.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReportesProductosController : ControllerBase
    {

        private readonly ForecastContext _context;
        private readonly GenerateReportesProductos _generateReportes;
        public ReportesProductosController(ForecastContext context, GenerateReportesProductos generateReportes)
        {

            _context = context;
            _generateReportes = generateReportes;

        }


        [HttpPost("proveedor")]
        public async Task<IActionResult> GenerateReportProvider(ReporteProductosProveedorRequest request)
        {

            var productos = await _context.Productos.Include(x => x.Grupos).Include(x => x.Proveedor).Where(x => x.Provider_id == request.Proveedor).ToListAsync();

            if (productos == null)
            {
                return NotFound();
            }

            var report = _generateReportes.GenerateReportProduct(productos, request);
            return Ok(report);
        }
    }
}