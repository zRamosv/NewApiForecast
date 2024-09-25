using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class ClientesInsert
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Contacto { get; set; }
    }
}