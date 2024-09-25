using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class GruposInsert
    {
        [Required]
        public string Clave { get; set; }
    }
}