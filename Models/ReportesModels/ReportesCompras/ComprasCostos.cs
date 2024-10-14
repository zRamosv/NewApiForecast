using iText.Commons.Utils;

namespace ApiForecast.Models.ReportesModels.ReportesCompras
{

    public class ReportByComprasCostosRequest
    {

        public string Grupo { get; set; }
        public int Producto { get; set; }
        public bool Detalle { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }

    public class ReportByComprasCostosResponse
    {

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<ReportByComprasCostosCompras> Compras { get; set; }
        public ReportByComprasCostosTotal Total { get; set; }
    }

    public class ReportByComprasCostosCompras
    {

        public DateOnly FechaCompra { get; set; }
        public ProductoInfo Producto { get; set; }
        public int Cantidad { get; set; }
        public ReportByComprasCostosCostosInfoUnit? CostosUSD { get; set; }
        public ReportByComprasCostosCostosInfoUnit? CostosMN { get; set; }
        public ReportByComprasCostoTotal? Detalles { get; set; }

    }
    public class ReportByComprasCostoTotal
    {
        public ProveedorInfoByProvider Proveedor { get; set; }
        public int Cantidad { get; set; }
        public ReportByComprasCostosCostosInfoUnit CostosUSD { get; set; }
        public ReportByComprasCostosCostosInfoUnit CostosMN { get; set; }
    }

    public class ReportByComprasCostosTotal
    {
        public int Cantidad { get; set; }
        public ReportByComprasCostosCostosInfo CostosUSD { get; set; }
        public ReportByComprasCostosCostosInfo CostosMN { get; set; }
    }
    public class ReportByComprasCostosCostosInfoUnit
    {
        public decimal CostoUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
    }
    public class ReportByComprasCostosCostosInfo
    {
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
    }
}