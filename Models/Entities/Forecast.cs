using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{

    public class Forecast{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_forecast { get; set; }
        public int Id_producto { get; set; }
        public int Meses { get; set; }
        public int Anio { get; set; }
        public int Cantidad_estimado { get; set; }
        public DateOnly Fecha_generacion { get; set; }
        public string Nombre_ejercicio { get; set; }
        public string Recomendaciones { get; set; }
    }
}