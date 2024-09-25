namespace ApiForecast.Models.DTOs
{

    public class UsuarioDTO
    {
        public int? User_Id { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public string? Foto_User { get; set; }
        public string? TelefonoUsuario { get; set; }
        public string? Contraseña { get; set; }
        public string? Estatus { get; set; }
        public bool? Vendedor_Modo { get; set; }
        public int? VendedorComision { get; set; }
        public string? VendedorClave { get; set; }
    }
}