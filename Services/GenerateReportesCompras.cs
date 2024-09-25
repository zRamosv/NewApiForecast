using ApiForecast.Data;
using ApiForecast.Models.ReportesModels.ReportesCompras;
using ApiForescast.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Services
{
    public class GenerateReportesCompras
    {
        private readonly ForecastContext _context;
        public GenerateReportesCompras(ForecastContext context)
{
            _context = context;
}
        public ReportResponse CreateReportVentasProductos(List<Compras> compras, ReportRequest request)
        {
       
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

            // Inicializamos
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

                // Sumamos totales
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

            // Resumen total
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

        public async Task<ReportByProviderResponse> CreateReportComprasByProvider(List<Compras> compras, ReportByProviderRequest request)
        {
            // Prepare the response
            var response = new ReportByProviderResponse
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin,
                Proveedor = new ProveedorInfoByProvider
                {
                   //placeholder
                },
                Compras = new List<CompraPorDiaByProvider>(),
                Total = new TotalInfoByProvider()
            };

            // Agrupar por fecha
            var groupedCompras = compras.GroupBy(c => c.Fecha);

            // Inicializamos
            int totalPiezas = 0;
            decimal totalSubtotalUSD = 0;
            decimal totalIVAUSD = 0;
            decimal totalTotalUSD = 0;
            decimal totalSubtotalMN = 0;
            decimal totalIVAMN = 0;
            decimal totalTotalMN = 0;

            foreach (var grupo in groupedCompras)
            {
                int dailyPiezas = 0;
                decimal dailySubtotalUSD = 0;
                decimal dailyIVAUSD = 0;
                decimal dailyTotalUSD = 0;
                decimal dailySubtotalMN = 0;
                decimal dailyIVAMN = 0;
                decimal dailyTotalMN = 0;

                var compraPorDia = new CompraPorDiaByProvider
                {
                    Fecha = grupo.Key.ToDateTime(new TimeOnly(0)),
                    Detalles = new List<DetalleCompraByProvider>()
                };

                foreach (var compra in grupo)
                {
                    dailyPiezas += compra.Cantidad;
                    var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Product_Id == compra.Product_id);

                    var detallesInfo = new DetalleCompraByProvider
                    {
                        Producto = new ProductoInfo
                        {
                            Id = compra.Product_id,
                            Clave = producto.Nombre
                        },
                        Piezas = compra.Cantidad,
                        ImportesUSD = new ImportesInfo(),
                        ImportesMN = new ImportesInfo()
                    };

                    if (compra.MonedaUSD)
                    {
                        detallesInfo.ImportesUSD = new ImportesInfo
                        {
                            Cantidad = compra.Cantidad,
                            Subtotal = compra.Precio * compra.Cantidad,
                            IVA = compra.Precio * compra.Cantidad * 0.16M,
                            Total = compra.Precio * compra.Cantidad
                        };

                        dailySubtotalUSD += detallesInfo.ImportesUSD.Subtotal;
                        dailyIVAUSD += detallesInfo.ImportesUSD.IVA;
                        dailyTotalUSD += detallesInfo.ImportesUSD.Total;
                    }
                    else
                    {
                        detallesInfo.ImportesMN = new ImportesInfo
                        {
                            Cantidad = compra.Cantidad,
                            Subtotal = compra.Precio * compra.Cantidad,
                            IVA = compra.Precio * compra.Cantidad * 0.16M,
                            Total = compra.Precio * compra.Cantidad
                        };

                        dailySubtotalMN += detallesInfo.ImportesMN.Subtotal;
                        dailyIVAMN += detallesInfo.ImportesMN.IVA;
                        dailyTotalMN += detallesInfo.ImportesMN.Total;
                    }

                    if (request.Detalle)
                    {
                        compraPorDia.Detalles.Add(detallesInfo);
                    }
                }

                compraPorDia.Piezas = dailyPiezas;
                compraPorDia.ImportesUSD = new ImportesInfo
                {
                    Subtotal = dailySubtotalUSD,
                    IVA = dailyIVAUSD,
                    Total = dailyTotalUSD
                };
                compraPorDia.ImportesMN = new ImportesInfo
                {
                    Subtotal = dailySubtotalMN,
                    IVA = dailyIVAMN,
                    Total = dailyTotalMN
                };

                totalPiezas += dailyPiezas;
                totalSubtotalUSD += dailySubtotalUSD;
                totalIVAUSD += dailyIVAUSD;
                totalTotalUSD += dailyTotalUSD;
                totalSubtotalMN += dailySubtotalMN;
                totalIVAMN += dailyIVAMN;
                totalTotalMN += dailyTotalMN;

                response.Compras.Add(compraPorDia);
            }

            // Total
            response.Total = new TotalInfoByProvider
            {
                Piezas = totalPiezas,
                ImportesUSD = new ImportesInfoByProvider
                {
                    Subtotal = totalSubtotalUSD,
                    IVA = totalIVAUSD,
                    Total = totalTotalUSD
                },
                ImportesMN = new ImportesInfoByProvider
                {
                    Subtotal = totalSubtotalMN,
                    IVA = totalIVAMN,
                    Total = totalTotalMN
                }
            };

            return response;
        }
    }
}