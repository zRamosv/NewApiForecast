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

                        Subtotal = compra.Precio * compra.Cantidad, //Calculamos subtotal
                        IVA = compra.Precio * compra.Cantidad * 0.16m, // IVA
                        Total = (compra.Precio * compra.Cantidad) + (compra.Precio * compra.Cantidad * 0.16m) // Total 
                    },
                    ImportesMN = new ImportesInfo
                    {

                        Subtotal = compra.Precio * compra.Cantidad,
                        IVA = compra.Precio * compra.Cantidad * 0.16m,
                        Total = (compra.Precio * compra.Cantidad) + (compra.Precio * compra.Cantidad * 0.16m)
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
        //TO DO
        public ReportByComprasCostosResponse CreateReportByComprasCostos(List<Compras> compras, ReportByComprasCostosRequest request)
        {
            int cantidadTotal = 0;
            decimal subtotalUSD = 0.00m;
            decimal subtotalMN = 0.00m;
            decimal ivaUSD = 0.00m;
            decimal ivaMN = 0.00m;
            decimal totalUSD = 0.00m;
            decimal totalMN = 0.00m;

            var response = new ReportByComprasCostosResponse
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin
            };

            foreach (var compra in compras)
            {
                var compraInfo = new ReportByComprasCostosCompras
                {
                    FechaCompra = compra.Fecha,
                    Producto = new ProductoInfo
                    {
                        Id = compra.Product_id,
                        Clave = compra.Product.Clave
                    },
                    Cantidad = compra.Cantidad
                };

                if (compra.MonedaUSD)
                {
                    compraInfo.CostosUSD = new ReportByComprasCostosCostosInfoUnit
                    {
                        CostoUnitario = compra.Precio,
                        Cantidad = compra.Cantidad,
                        Subtotal = compra.Precio * compra.Cantidad,
                        IVA = compra.Precio * compra.Cantidad * 0.16m,
                        Total = (compra.Precio * compra.Cantidad) + (compra.Precio * compra.Cantidad * 0.16m)
                    };

                    subtotalUSD += compraInfo.CostosUSD.Subtotal;
                    ivaUSD += compraInfo.CostosUSD.IVA;
                    totalUSD += compraInfo.CostosUSD.Total;
                }
                else
                {
                    compraInfo.CostosMN = new ReportByComprasCostosCostosInfoUnit
                    {
                        CostoUnitario = compra.Precio,
                        Cantidad = compra.Cantidad,
                        Subtotal = compra.Precio * compra.Cantidad,
                        IVA = compra.Precio * compra.Cantidad * 0.16m,
                        Total = (compra.Precio * compra.Cantidad) + (compra.Precio * compra.Cantidad * 0.16m)
                    };

                    subtotalMN += compraInfo.CostosMN.Subtotal;
                    ivaMN += compraInfo.CostosMN.IVA;
                    totalMN += compraInfo.CostosMN.Total;
                }

                if (request.Detalle)
                {
                    compraInfo.Detalles = new ReportByComprasCostoTotal
                    {
                        Proveedor = new ProveedorInfoByProvider
                        {
                            Id = compra.Provider_id,
                            Nombre = compra.Proveedor.Nombre
                        },
                        Cantidad = compra.Cantidad,
                        CostosUSD = compraInfo.CostosUSD,
                        CostosMN = compraInfo.CostosMN
                    };
                }

                response.Compras.Add(compraInfo);
                cantidadTotal += compra.Cantidad;
            }

            response.Total = new ReportByComprasCostosTotal
            {
                Cantidad = cantidadTotal,
                CostosUSD = new ReportByComprasCostosCostosInfo
                {
                    Subtotal = subtotalUSD,
                    IVA = ivaUSD,
                    Total = totalUSD
                },
                CostosMN = new ReportByComprasCostosCostosInfo
                {
                    Subtotal = subtotalMN,
                    IVA = ivaMN,
                    Total = totalMN
                }
            };

            return response;
        }


        public ReportDTO CreateReportPeriodo(List<Compras> compras, bool includeDetails)
        {
            var reportDTO = new ReportDTO();
            var totalUSD = new ImportesDTO();
            var totalMN = new ImportesDTO();
            int totalPiezas = 0;

            foreach (var compra in compras)
            {
                var compraReport = new CompraReportDTO
                {
                    Recepcion = compra.Fecha.ToDateTime(new TimeOnly(0)),
                    Proveedor = compra.Provider_id,
                    Estado = "REGISTRADA",
                    Piezas = compra.Cantidad,//////
                    ImportesUSD = new ImportesDTO(),
                    ImportesMN = new ImportesDTO(),
                    Detalles = includeDetails ? new List<DetalleDTO>() : null
                };

                if (compra.MonedaUSD)
                {
                    compraReport.ImportesUSD = new ImportesDTO
                    {

                        Importe = (double)compra.Precio * compra.Cantidad,
                        IVA = (double)(compra.Precio * compra.Cantidad * 0.16M), // 16% IVA
                        Total = (double)(compra.Precio * compra.Cantidad * 1.16M)
                    };

                    totalUSD.Importe += compraReport.ImportesUSD.Importe;
                    totalUSD.IVA += compraReport.ImportesUSD.IVA;
                    totalUSD.Total += compraReport.ImportesUSD.Total;
                }
                else
                {
                    compraReport.ImportesMN = new ImportesDTO
                    {

                        Importe = (double)compra.Precio * compra.Cantidad,
                        IVA = (double)(compra.Precio * compra.Cantidad * 0.16M),
                        Total = (double)(compra.Precio * compra.Cantidad * 1.16M)
                    };

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
                        Piezas = compra.Cantidad,//////
                        ImportesUSD = compra.MonedaUSD ? compraReport.ImportesUSD : new ImportesDTO(),
                        ImportesMN = !compra.MonedaUSD ? compraReport.ImportesMN : new ImportesDTO()
                    };
                    compraReport.Detalles.Add(detail);
                }

                reportDTO.Compras.Add(compraReport);
                totalPiezas += compra.Cantidad;
            }


            reportDTO.Total = new TotalDTO
            {
                Piezas = totalPiezas,
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

                            Subtotal = compra.Precio * compra.Cantidad,
                            IVA = compra.Precio * compra.Cantidad * 0.16M,
                            Total = (compra.Precio * compra.Cantidad) + (compra.Precio * compra.Cantidad * 0.16M)
                        };

                        dailySubtotalUSD += detallesInfo.ImportesUSD.Subtotal;
                        dailyIVAUSD += detallesInfo.ImportesUSD.IVA;
                        dailyTotalUSD += detallesInfo.ImportesUSD.Total;
                    }
                    else
                    {
                        detallesInfo.ImportesMN = new ImportesInfo
                        {

                            Subtotal = compra.Precio * compra.Cantidad,
                            IVA = compra.Precio * compra.Cantidad * 0.16M,
                            Total = (compra.Precio * compra.Cantidad) + (compra.Precio * compra.Cantidad * 0.16M)
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