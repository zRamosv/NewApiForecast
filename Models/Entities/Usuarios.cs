using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ApiForecast.Models.Entities
{

    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [JsonIgnore]

        public ICollection<AccessoSucursales> AccessoSucursales { get; set; }
        [JsonIgnore]
        public ICollection<Permisos> Permisos { get; set; }
        [JsonIgnore]
        public ICollection<UserRoles> UserRoles { get; set; }
        public bool VerifyPassword(string password){
            return BCrypt.Net.BCrypt.Verify(password, Contraseña);
        }
    }
}