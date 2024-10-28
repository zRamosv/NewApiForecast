using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.DTOs{

    public class ForecastDTO{
        [Required]
        public int Group_id { get; set; }
        [Required]
        public int Meses_Forecast { get; set; } 
        [Required]
        [StringLength(100)]
        public required string Nombre_Ejercicio { get; set; }
    }
}