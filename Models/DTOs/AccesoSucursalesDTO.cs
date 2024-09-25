using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs
{

    public class AccesoSucursalesDTO
    {
        public int? Acceso_id { get; set; }
        public int? User_id { get; set; }
        public Usuarios? Usuario { get; set; }
        public string? Nombre_user { get; set; }
        public int? Sucursal_id { get; set; }
        public Sucursales? Sucursal { get; set; }

    }
}