using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{

    public class DetalleForecast{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleForecastID {get; set;}
        public int Id_Producto {get; set;}
        public Productos Producto {get; set;}
        public int Anio {get; set;}
        public decimal Cantidad_estimado {get; set;}
        public int Total_en_transito {get; set;}
        public string Nombre_Ejercicio {get; set;}
        public string Recomendaciones {get; set;}
    }
}