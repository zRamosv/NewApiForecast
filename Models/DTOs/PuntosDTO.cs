using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs
{

    public class PuntosDTO
    {
        public int? Points_id { get; set; }
        public decimal? PesosPorPunto { get; set; }
        public int? PuntosMinimos { get; set; }
        public int? Sucursal_id { get; set; }
        public Sucursales? Sucursales { get; set; }

    }
}