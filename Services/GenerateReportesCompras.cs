using ApiForecast.Models.ReportesModels.ReportesCompras;
using ApiForescast.Models.Entities;

namespace ApiForecast.Services
{
    public class GenerateReportesCompras
    {


        public ReportResponse CreateReportVentasProductos(List<Compras> compras, ReportRequest request)
        {
            // Respuesta
            var response = new ReportResponse
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin,
                Producto = new ProductoInfo
                {
                    Id = request.Producto,

                },
                Compras = new List<CompraInfo>(),
                Total = new TotalInfo()
            };

            // Calculate totals
            int totalCantidad = 0;
            decimal totalSubtotalUSD = 0;
            decimal totalIVAUSD = 0;
            decimal totalTotalUSD = 0;
            decimal totalSubtotalMN = 0;
            decimal totalIVAMN = 0;
            decimal totalTotalMN = 0;

            foreach (var compra in compras)
            {
                var compraInfo = new CompraInfo
                {
                    Fecha = compra.Fecha.ToDateTime(new TimeOnly(0)),
                    Cantidad = compra.Cantidad,
                    ImportesUSD = new ImportesInfo
                    {
                        Cantidad = compra.Cantidad,
                        Subtotal = compra.Precio * compra.Cantidad, //Calculamos subtotal
                        IVA = compra.Precio * compra.Cantidad * 0.16m, // IVA
                        Total = compra.Precio * compra.Cantidad // Total 
                    },
                    ImportesMN = new ImportesInfo
                    {
                        Cantidad = compra.Cantidad,
                        Subtotal = compra.Precio * compra.Cantidad,
                        IVA = compra.Precio * compra.Cantidad * 0.16m,
                        Total = compra.Precio * compra.Cantidad
                    },
                    Detalles = request.Detalle ? new DetallesInfo { Proveedor = compra.Provider_id } : null
                };

                // Update totals
                totalCantidad += compra.Cantidad;
                if (compra.MonedaUSD)
                {
                    totalSubtotalUSD += compraInfo.ImportesUSD.Subtotal;
                    totalIVAUSD += compraInfo.ImportesUSD.IVA;
                    totalTotalUSD += compraInfo.ImportesUSD.Total;
                }
                else
                {
                    totalSubtotalMN += compraInfo.ImportesMN.Subtotal;
                    totalIVAMN += compraInfo.ImportesMN.IVA;
                    totalTotalMN += compraInfo.ImportesMN.Total;
                }

                response.Compras.Add(compraInfo);
            }

            // Set total summary
            response.Total = new TotalInfo
            {
                Cantidad = totalCantidad,
                ImportesUSD = new ImportesInfo
                {
                    Subtotal = totalSubtotalUSD,
                    IVA = totalIVAUSD,
                    Total = totalTotalUSD
                },
                ImportesMN = new ImportesInfo
                {
                    Subtotal = totalSubtotalMN,
                    IVA = totalIVAMN,
                    Total = totalTotalMN
                }
            };

            return response;

        }

        public ReportDTO CreateReportPeriodo(List<Compras> compras, bool includeDetails)
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