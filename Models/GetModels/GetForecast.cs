using ApiForecast.Models.Entities;

namespace ApiForecast.Models.GetModels
{
    public class GetForecast
    {
    
        public int Id_forecast { get; set; }
     
    
        public int Group_id { get; set; }
        public Grupos Group { get; set; }
        public int Meses { get; set; }
        public int Anio { get; set; }
        public int Cantidad_estimado { get; set; }
        public DateOnly Fecha_generacion { get; set; }
        public string Nombre_ejercicio { get; set; }
        public string Recomendaciones { get; set; }
    }
}