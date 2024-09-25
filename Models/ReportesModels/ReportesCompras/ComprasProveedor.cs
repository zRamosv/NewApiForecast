namespace ApiForecast.Models.ReportesModels.ReportesCompras
{

    public class ReportByProviderRequest
    {
        public int IdProveedor { get; set; }
        public string Moneda { get; set; } // "USD", "MN", "General"
        public bool Detalle { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ReportByProviderResponse
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public ProveedorInfoByProvider Proveedor { get; set; }
        public List<CompraPorDiaByProvider> Compras { get; set; }
        public TotalInfoByProvider Total { get; set; }
    }

    public class ProveedorInfoByProvider
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CompraPorDiaByProvider
    {
        public DateTime Fecha { get; set; }
        public int Piezas { get; set; }
        public ImportesInfo ImportesUSD { get; set; }
        public ImportesInfo ImportesMN { get; set; }
        public List<DetalleCompraByProvider> Detalles { get; set; }
    }

    public class DetalleCompraByProvider
    {
        public ProductoInfo Producto { get; set; }
        public int Piezas { get; set; }
        public ImportesInfo ImportesUSD { get; set; }
        public ImportesInfo ImportesMN { get; set; }
    }

    public class ProductoInfoByProvider
    {
        public int Id { get; set; }
        public string Clave { get; set; }
    }

    public class ImportesInfoByProvider
    {
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
    }

    public class TotalInfoByProvider
    {
        public int Piezas { get; set; }
        public ImportesInfoByProvider ImportesUSD { get; set; }
        public ImportesInfoByProvider ImportesMN { get; set; }
    }

}