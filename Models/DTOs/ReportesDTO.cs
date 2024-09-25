namespace ApiForecast.Models.DTOs
{
    public class ReportesDTO
    {
        public int? Report_id { get; set; }
        public string? Tipo { get; set; }
        public DateOnly? Fecha_inicio { get; set; }
        public DateOnly? Fecha_fin { get; set; }
        public string? Detalles { get; set; }
    }
}