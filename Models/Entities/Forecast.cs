using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{

    public class Forecast{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_forecast { get; set; }
        [JsonIgnore]
        public int Id_producto { get; set; }
        public Productos Producto { get; set; }
        public int Meses { get; set; }
        public int Anio { get; set; }
        public int Cantidad_estimado { get; set; }
        public DateOnly Fecha_generacion { get; set; }
        public string Nombre_ejercicio { get; set; }
        public string Recomendaciones { get; set; }
    }
}