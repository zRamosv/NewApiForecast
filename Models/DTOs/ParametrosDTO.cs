using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs{


    public class ParametrosDTO{


        public int? Parameters_id {get; set;}
        public int? Sucursal_id {get; set;}
        public Sucursales? Sucursal {get; set;}
        public string? FirmaSupervisor {get; set;}
        public string? ExistenciasRequeridas {get; set;}
    }
}