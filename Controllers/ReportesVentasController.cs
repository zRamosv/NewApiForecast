using ApiForecast.Data;
using ApiForecast.Services;
using ApiForescast.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class ReportesVentasController : ControllerBase
    {
        private readonly ForecastContext _context;
      

        public ReportesVentasController(ForecastContext context, GeneratePDF generatePDF)
        {
            _context = context;
            
        }


        [HttpPost("GenerateReport")]
        public async Task<IActionResult> GenerateReport([FromBody] ReportInputModel input)
        {

            var compras = await _context.Compras
                .Include(c => c.Product)
                .Where(c => c.Fecha >= DateOnly.FromDateTime(input.FechaInicio) && c.Fecha <= DateOnly.FromDateTime(input.FechaFin))
                .ToListAsync();


            var report = CreateReport(compras, input.Detalle);
            report.FechaInicio = input.FechaInicio;
            report.FechaFin = input.FechaFin;

            return Ok(report);


        }
    

        private ReportDTO CreateReport(List<Compras> compras, bool includeDetails)
        {
            var reportDTO = new ReportDTO();
            var totalUSD = new ImportesDTO();
            var totalMN = new ImportesDTO();


            foreach (var compra in compras)
            {
                var compraReport = new CompraReportDTO
                {
                    Recepcion = compra.Fecha.ToDateTime(new TimeOnly(0)),
                    Proveedor = compra.Provider_id,
                    Estado = "REGISTRADA",
                    ImportesUSD = new ImportesDTO(),
                    ImportesMN = new ImportesDTO(),
                    Detalles = includeDetails ? new List<DetalleDTO>() : null
                };

                if (compra.MonedaUSD)
                {
                    compraReport.ImportesUSD = new ImportesDTO
                    {
                        Piezas = compra.Cantidad,
                        Importe = (double)compra.Precio * compra.Cantidad,
                        IVA = (double)(compra.Precio * compra.Cantidad * 0.16M), // 16% IVA
                        Total = (double)(compra.Precio * compra.Cantidad * 1.16M)
                    };
                    totalUSD.Piezas += compra.Cantidad;
                    totalUSD.Importe += compraReport.ImportesUSD.Importe;
                    totalUSD.IVA += compraReport.ImportesUSD.IVA;
                    totalUSD.Total += compraReport.ImportesUSD.Total;
                }
                else
                {
                    compraReport.ImportesMN = new ImportesDTO
                    {
                        Piezas = compra.Cantidad,
                        Importe = (double)compra.Precio * compra.Cantidad,
                        IVA = (double)(compra.Precio * compra.Cantidad * 0.16M),
                        Total = (double)(compra.Precio * compra.Cantidad * 1.16M)
                    };
                    totalMN.Piezas += compra.Cantidad;
                    totalMN.Importe += compraReport.ImportesMN.Importe;
                    totalMN.IVA += compraReport.ImportesMN.IVA;
                    totalMN.Total += compraReport.ImportesMN.Total;
                }

                if (includeDetails)
                {
                    var detail = new DetalleDTO
                    {
                        Clave = compra.Product.Clave,
                        Producto = compra.Product_id,
                        ImportesUSD = compra.MonedaUSD ? compraReport.ImportesUSD : new ImportesDTO(),
                        ImportesMN = !compra.MonedaUSD ? compraReport.ImportesMN : new ImportesDTO()
                    };
                    compraReport.Detalles.Add(detail);
                }

                reportDTO.Compras.Add(compraReport);
            }


            reportDTO.Total = new TotalDTO
            {
                Piezas = totalUSD.Piezas + totalMN.Piezas,
                ImportesUSD = totalUSD,
                ImportesMN = totalMN
            };

            return reportDTO;
        }


    }
}