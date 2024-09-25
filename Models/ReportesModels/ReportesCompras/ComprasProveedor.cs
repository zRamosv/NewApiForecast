namespace ApiForecast.Models.ReportesModels.ReportesCompras
{

    public class ReportByProviderRequest
    {
        public int Proveedor { get; set; }
        public string Moneda { get; set; } // "USD", "MN", "General"
        public bool Detalle { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ReportByProviderResponse
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public ProveedorInfo Proveedor { get; set; }
        public List<CompraPorDia> Compras { get; set; }
        public TotalInfo Total { get; set; }
    }

    public class ProveedorInfo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CompraPorDia
    {
        public DateTime Fecha { get; set; }
        public int Piezas { get; set; }
        public ImportesInfo ImportesUSD { get; set; }
        public ImportesInfo ImportesMN { get; set; }
        public List<DetalleCompra> Detalles { get; set; }
    }

    public class DetalleCompra
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
        public double Subtotal { get; set; }
        public double IVA { get; set; }
        public double Total { get; set; }
    }

    public class TotalInfoByProvider
    {
        public int Piezas { get; set; }
        public ImportesInfo ImportesUSD { get; set; }
        public ImportesInfo ImportesMN { get; set; }
    }

}