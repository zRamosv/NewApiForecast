using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs{

    public class PermisosDTO{

        public int? Permiso_id {get;set;}
        public int? User_id {get;set;}
        public Usuarios? User {get;set;}
        public string? Modulo {get;set;}
        public string? Nivel_acceso {get;set;}
    }
}