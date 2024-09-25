using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;

namespace ApiForecast.Models.GetModels
{

    public class GetUserRoles
    {

        public int User_Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Foto_User { get; set; }
        public string Telefono_Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Estatus { get; set; }
        public bool Vendedor_Modo { get; set; }
        public int VendedorComision { get; set; }
        public string VendedorClave { get; set; }
       
        
        public ICollection<UserRolesDTO> UserRoles { get; set; }
    }
}