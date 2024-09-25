
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
    public List<CompraReportDTO> Compras { get; set; } = new List<CompraReportDTO>(); // List of purchases within the report
    public TotalDTO Total { get; set; } // Totals for the report
}

public class CompraReportDTO
{
    public DateTime Recepcion { get; set; } // Purchase date
    public int Proveedor { get; set; } // Provider ID
    public string Estado { get; set; } // Status (e.g., REGISTRADA)
    public ImportesDTO ImportesUSD { get; set; } // Calculated totals for USD purchases
    public ImportesDTO ImportesMN { get; set; } // Calculated totals for MXN purchases
    public List<DetalleDTO> Detalles { get; set; } // List of purchase details (empty if not requested)
}

public class ImportesDTO
{
    public int Piezas { get; set; } // Number of units/items
    public double Importe { get; set; } // Total value before tax (IVA)
    public double IVA { get; set; } // Tax amount (IVA)
    public double Total { get; set; } // Total value including tax (IVA)
}

public class DetalleDTO
{
    public string Clave { get; set; } // Product key
    public int Producto { get; set; } // Product ID
    public ImportesDTO ImportesUSD { get; set; } // USD totals for this product
    public ImportesDTO ImportesMN { get; set; } // MXN totals for this product
}

public class TotalDTO
{
    public int Piezas { get; set; } // Total number of units/items for all purchases
    public ImportesDTO ImportesUSD { get; set; } // Total USD amounts
    public ImportesDTO ImportesMN { get; set; } // Total MXN amounts
}