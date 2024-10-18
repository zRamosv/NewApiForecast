using ApiForecast.Data;
using ApiForecast.Models.Entities;
using ApiForecast.Models.ReportesModels.ReportesCompras;
using ApiForecast.Models.ReportesModels.ReportesProductos;

namespace ApiForecast.Services
{

    public class GenerateReportesProductos
    {

        private readonly ForecastContext _context;
        public GenerateReportesProductos(ForecastContext context)
        {

            _context = context;
        }

        public ReporteProductosProveedorResponse GenerateReportProduct(List<Productos> productos, ReporteProductosProveedorRequest request)
        {

            var report = new ReporteProductosProveedorResponse();
            if (request.Agrupacion == "Proveedor")
            {
                var proveedorInfo = new ProveedorInfoByProvider
                {
                    Id = productos.First().Proveedor.Provider_id,
                    Nombre = productos.First().Proveedor.Nombre
                };
                report.Productos.Add(new ReporteProductosProveedorProducts
                {
                    Proveedor = proveedorInfo,
                    Productos = productos.Select(x => new ReporteProductosProveedorProductoInfo
                    {
                        Id = x.Product_Id,
                        Clave = x.Clave,
                        Precio = x.Precio,
                        Stock = x.Stock,
                        Categoria = x.Categoria
                    }).ToList()

                });

                return report;

            }
            else if (request.Agrupacion == "ProveedorGrupo")
            {
                var groupedCompras = productos.GroupBy(c => c.Grupos);

                foreach (var grupo in groupedCompras)
                {
                    var productoPorGrupo = new ReporteProductosProveedorProducts
                    {
                        Proveedor = new ProveedorInfoByProvider
                        {
                            Id = grupo.Key.Group_id,
                            Nombre = grupo.Key.Clave
                        },
                        Grupo = new ReporteProductosProveedorGrupoInfo
                        {
                            Id = grupo.Key.Group_id,
                            Nombre = grupo.Key.Clave
                        },
                        Productos = grupo.Key.Productos.Select(x => new ReporteProductosProveedorProductoInfo
                        {
                            Id = x.Product_Id,
                            Clave = x.Clave,
                            Precio = x.Precio,
                            Stock = x.Stock,
                            Categoria = x.Categoria
                        }).ToList()

                    };

                    report.Productos.Add(productoPorGrupo);

                }
                return report;
            }
            return report;
        }

    }
}
