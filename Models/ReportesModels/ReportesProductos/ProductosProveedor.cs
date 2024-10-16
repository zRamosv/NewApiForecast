namespace ApiForecast.Models.ReportesModels.ReportesProductos{


    public class ReporteProductosProveedorRequest{
        public int Proveedor { get; set; }
        public int Grupo { get; set; }
        public required string Agrupacion { get; set; }
    }
}