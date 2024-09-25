using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class PermisosInsert
    {
        [Required]
        public int User_id { get; set; }
        [Required]

        public string Modulo { get; set; }
        [Required]
        public string Nivel_acceso { get; set; }
    }
}
