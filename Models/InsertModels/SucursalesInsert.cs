using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class SucursalesInsert
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
    }
}