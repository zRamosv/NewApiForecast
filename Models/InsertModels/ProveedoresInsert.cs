using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class ProveedoresInsert
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Contacto { get; set; }

    }
}