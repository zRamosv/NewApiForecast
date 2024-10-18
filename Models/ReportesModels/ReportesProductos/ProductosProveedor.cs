using ApiForecast.Models.ReportesModels.ReportesCompras;

namespace ApiForecast.Models.ReportesModels.ReportesProductos
{


    public class ReporteProductosProveedorRequest
    {
        public int Proveedor { get; set; }

        public required string Agrupacion { get; set; }
    }


    public class ReporteProductosProveedorResponse
    {

        public List<ReporteProductosProveedorProducts> Productos { get; set; } = new List<ReporteProductosProveedorProducts>();

    }

    public class ReporteProductosProveedorProducts
    {

        public ProveedorInfoByProvider? Proveedor { get; set; }
        public ReporteProductosProveedorGrupoInfo? Grupo { get; set; }
        public List<ReporteProductosProveedorProductoInfo> Productos { get; set; } = new List<ReporteProductosProveedorProductoInfo>();
    }
    public class ReporteProductosProveedorGrupoInfo
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
    }
    public class ReporteProductosProveedorProductoInfo
    {
        public int Id { get; set; }
        public required string Clave { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public required string Categoria { get; set; }
    }
}

