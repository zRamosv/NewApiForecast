using ApiForecast.Data;
using ApiForecast.Models.ReportesModels.ReportesCompras;
using ApiForecast.Services;
using ApiForescast.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ReporteComprasController : ControllerBase
    {
        private readonly ForecastContext _context;
        private readonly GenerateReportesCompras _generateReportesCompras;

        public ReporteComprasController(ForecastContext context, GenerateReportesCompras generateReportesCompras)
        {
            _context = context;
            _generateReportesCompras = generateReportesCompras;

        }


        [HttpPost("periodo")]
        public async Task<IActionResult> GenerateReport([FromBody] ReportInputModel input)
        {

            var compras = await _context.Compras
                .Include(c => c.Product)
                .Where(c => c.Fecha >= DateOnly.FromDateTime(input.FechaInicio) && c.Fecha <= DateOnly.FromDateTime(input.FechaFin))
                .ToListAsync();


            var report = _generateReportesCompras.CreateReportPeriodo(compras, input.Detalle);
            report.FechaInicio = input.FechaInicio;
            report.FechaFin = input.FechaFin;

            return Ok(report);


        }

        [HttpPost("producto")]
        public async Task<IActionResult> GeneratePurchaseReport([FromBody] ReportRequest request)
        {

            if (request.FechaInicio > request.FechaFin)
                return BadRequest("Fecha inicio debe ser anterior a la fecha fin");
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Product_Id == request.Producto);
            if (producto == null)
                return NotFound("No se encontro el producto");
            //  Obtenemos las compras dependediendo de criterios
            var comprasQuery = _context.Compras
                .Where(c => c.Product_id == request.Producto
                             && c.Fecha >= DateOnly.FromDateTime(request.FechaInicio)
                             && c.Fecha <= DateOnly.FromDateTime(request.FechaFin));

            // Filtros de Moneda
            if (request.Moneda == "USD")
            {
                comprasQuery = comprasQuery.Where(c => c.MonedaUSD); // Solo compras en Dolares
            }
            else if (request.Moneda == "MN")
            {
                comprasQuery = comprasQuery.Where(c => !c.MonedaUSD); // Solo compras en Pesos
            }
            else if (request.Moneda == "General") // Las 2
            {
                comprasQuery = comprasQuery.Where(c => c.MonedaUSD || !c.MonedaUSD);
            }

            var compras = await comprasQuery.ToListAsync();


            var report = _generateReportesCompras.CreateReportVentasProductos(compras, request);
            report.Producto.Clave = producto.Clave;

            return Ok(report);
        }

        [HttpPost("proveedor")]
        public async Task<IActionResult> GenerateSupplierReport([FromBody] ReportByProviderRequest request)
        {


            if (request.FechaInicio > request.FechaFin)
            {
                return BadRequest("Fecha inicio debe ser anterior a la fecha fin");
            }
            var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Provider_id == request.IdProveedor);
            if (proveedor == null)
            {
                return NotFound("No se encontro el proveedor");
            }
            // Obtenemos las compras dependediendo de criterios
            var comprasQuery = _context.Compras
                .Where(c => c.Provider_id == request.IdProveedor
                             && c.Fecha >= DateOnly.FromDateTime(request.FechaInicio)
                             && c.Fecha <= DateOnly.FromDateTime(request.FechaFin));

            // Filtros de moneda
            if (request.Moneda == "USD")
            {
                comprasQuery = comprasQuery.Where(c => c.MonedaUSD); // Solo compras en Dolares
            }
            else if (request.Moneda == "MN")
            {
                comprasQuery = comprasQuery.Where(c => !c.MonedaUSD); // Solo compras en Pesos
            }
            else if (request.Moneda == "General") // Las 2
            {
                comprasQuery = comprasQuery.Where(c => c.MonedaUSD || !c.MonedaUSD);
            }
            var compras = await comprasQuery.ToListAsync();
            var report = await _generateReportesCompras.CreateReportComprasByProvider(compras, request);
            report.Proveedor.Id = request.IdProveedor;
            report.Proveedor.Nombre = proveedor.Nombre;
            

            return Ok(report);
        }

    }
}