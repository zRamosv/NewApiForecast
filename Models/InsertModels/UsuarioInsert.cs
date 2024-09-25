using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class UsuarioInsert
    {
        [Required]
        public string Clave { get; set; }
        [Required]
        public string Nombre { get; set; }
        
        [Required]
        [Phone]
        public string TelefonoUsuario { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public string Estatus { get; set; }
        [Required]
        public bool Vendedor_Modo { get; set; }
        [Required]
        public int VendedorComision { get; set; }
        [Required]
        public string VendedorClave { get; set; }
    }
}