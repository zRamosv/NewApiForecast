using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class AccesoSucursalesInsert
    {
        [Required]
        public int User_id { get; set; }
        [Required]
        public string Nombre_user { get; set; }
        [Required]
        public int Sucursal_id { get; set; }
    }
}