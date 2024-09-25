using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class ReportesInsert
    {
        [Required]
        public string Tipo { get; set; }
        [Required]
        public DateOnly Fecha_inicio { get; set; }
        [Required]
        public DateOnly Fecha_fin { get; set; }
        [Required]
        public string Detalles { get; set; }
    }
}