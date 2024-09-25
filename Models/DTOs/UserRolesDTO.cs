using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs
{

    public class UserRolesDTO
    {
        public int? Id { get; set; }
        public int? User_Id { get; set; }
        public Usuarios? User { get; set; }
        public int? Role_Id { get; set; }
        public Roles? Role { get; set; }
        public int? Sucursal_id { get; set; }
        public Sucursales? Sucursal { get; set; }
    }
}