namespace ApiForecast.Models.ReportesModels.ReportesCompras
{
    public class ReportRequest
    {
        public int Grupo { get; set; }
        public int Producto { get; set; }
        public string Moneda { get; set; }
        public bool Detalle { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ReportResponse
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public ProductoInfo Producto { get; set; } // Info producto
        public List<CompraInfo> Compras { get; set; } // Lista compras
        public TotalInfo Total { get; set; } // Resumen total
    }

    public class ProductoInfo
    {
        public int Id { get; set; } //id
        public string Clave { get; set; } // clave
    }

    public class CompraInfo
    {
        public DateTime Fecha { get; set; } // fecha compra
        public int Cantidad { get; set; } // cantidad
        public ImportesInfo ImportesUSD { get; set; } // usd
        public ImportesInfo ImportesMN { get; set; } // mn
        public DetallesInfo Detalles { get; set; } // detalles
    }

    public class ImportesInfo
    {
        public int Cantidad { get; set; } // cantidad
        public decimal Subtotal { get; set; } // subtotal pre impuesto
        public decimal IVA { get; set; } // impuestos
        public decimal Total { get; set; } // Total 
    }

    public class DetallesInfo
    {
        public int Proveedor { get; set; } // id proveedor
    }

    public class TotalInfo
    {
        public int Cantidad { get; set; } // cantidad total
        public ImportesInfo ImportesUSD { get; set; } // total en dolares
        public ImportesInfo ImportesMN { get; set; } // total en pesos
    }
}