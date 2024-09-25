
namespace ApiForecast.Models.ReportesModels.ReportesCompras
{
    public class ReportInputModel
    {
        public string Transaccion { get; set; }
        public bool Detalle { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ReportDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<CompraReportDTO> Compras { get; set; } = new List<CompraReportDTO>(); // Lista de compras con el reporte
        public TotalDTO Total { get; set; } // Total
    }

    public class CompraReportDTO
    {
        public DateTime Recepcion { get; set; } // Fechad e compra
        public int Proveedor { get; set; } // Id prov
        public string Estado { get; set; } // Estado (e.g., REGISTRADA)
        public ImportesDTO ImportesUSD { get; set; } // Total de Compras USD
        public ImportesDTO ImportesMN { get; set; } // Totales de Compras MXN
        public List<DetalleDTO> Detalles { get; set; } // Lista de detalles de la compra
    }

    public class ImportesDTO
    {
        public int Piezas { get; set; }
        public double Importe { get; set; } // Total antes de IVA
        public double IVA { get; set; } // IVA
        public double Total { get; set; } // TOtal con iva
    }

    public class DetalleDTO
    {
        public string Clave { get; set; } // clave producto
        public int Producto { get; set; } // id producto
        public ImportesDTO ImportesUSD { get; set; } // Total USD de este producto
        public ImportesDTO ImportesMN { get; set; } // Total MXN de este producto
    }

    public class TotalDTO
    {
        public int Piezas { get; set; } // Numero total de todas las piezas
        public ImportesDTO ImportesUSD { get; set; } // Total USD de todas las piezas
        public ImportesDTO ImportesMN { get; set; } // Total MXN de todas las piezas 
    }
}