using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Globalization;
using ApiForecast.Models.ReportesModels.ReportesCompras;

namespace ApiForecast.Services{

    public class GeneratePDF{
         public byte[] GenerateReportPdf(ReportDTO report)
    {
        using (var memoryStream = new MemoryStream())
        {
            // Initialize PDF writer
            using (var writer = new PdfWriter(memoryStream))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Add title to PDF
                    document.Add(new Paragraph("Report Summary").SetFontSize(18));

                    // Add Date range
                    document.Add(new Paragraph($"Fecha Inicio: {report.FechaInicio:yyyy-MM-dd}"));
                    document.Add(new Paragraph($"Fecha Fin: {report.FechaFin:yyyy-MM-dd}"));

                    // Add Purchases (Compras) details
                    foreach (var compra in report.Compras)
                    {
                        document.Add(new Paragraph($"Recepción: {compra.Recepcion:yyyy-MM-dd}"));
                        document.Add(new Paragraph($"Proveedor ID: {compra.Proveedor}"));
                        document.Add(new Paragraph($"Estado: {compra.Estado}"));

                        // ImportesUSD
                        document.Add(new Paragraph("Importes in USD:"));
                        document.Add(new Paragraph($"Piezas: {compra.ImportesUSD.Piezas}"));
                        document.Add(new Paragraph($"Importe: {compra.ImportesUSD.Importe.ToString("C", CultureInfo.CurrentCulture)}"));
                        document.Add(new Paragraph($"IVA: {compra.ImportesUSD.IVA.ToString("C", CultureInfo.CurrentCulture)}"));
                        document.Add(new Paragraph($"Total: {compra.ImportesUSD.Total.ToString("C", CultureInfo.CurrentCulture)}"));

                        // ImportesMN
                        document.Add(new Paragraph("Importes in MN:"));
                        document.Add(new Paragraph($"Piezas: {compra.ImportesMN.Piezas}"));
                        document.Add(new Paragraph($"Importe: {compra.ImportesMN.Importe.ToString("C", CultureInfo.CurrentCulture)}"));
                        document.Add(new Paragraph($"IVA: {compra.ImportesMN.IVA.ToString("C", CultureInfo.CurrentCulture)}"));
                        document.Add(new Paragraph($"Total: {compra.ImportesMN.Total.ToString("C", CultureInfo.CurrentCulture)}"));
                        
                        // Add details if requested
                        if (compra.Detalles?.Any() == true)
                        {
                            document.Add(new Paragraph("Detalles:"));
                            foreach (var detalle in compra.Detalles)
                            {
                                document.Add(new Paragraph($"Producto ID: {detalle.Producto}"));
                                document.Add(new Paragraph($"Clave: {detalle.Clave}"));

                                document.Add(new Paragraph($"Importe USD: {detalle.ImportesUSD.Importe.ToString("C", CultureInfo.CurrentCulture)}"));
                                document.Add(new Paragraph($"Importe MN: {detalle.ImportesMN.Importe.ToString("C", CultureInfo.CurrentCulture)}"));
                            }
                        }

                        document.Add(new Paragraph("\n"));
                    }

                    // Add Total Summary
                    document.Add(new Paragraph("Total Summary:"));
                    document.Add(new Paragraph($"Total Piezas: {report.Total.Piezas}"));
                    document.Add(new Paragraph($"Total Importe USD: {report.Total.ImportesUSD.Importe.ToString("C", CultureInfo.CurrentCulture)}"));
                    document.Add(new Paragraph($"Total Importe MN: {report.Total.ImportesMN.Importe.ToString("C", CultureInfo.CurrentCulture)}"));

                    // Close document
                    document.Close();
                }
            }

            return memoryStream.ToArray();
        }
    }
    }
}