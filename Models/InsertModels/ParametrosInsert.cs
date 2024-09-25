using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class ParametrosInsert
    {
        [Required]
        public int Sucursal_id { get; set; }
        [Required]

        public string FirmaSupervisor { get; set; }
        [Required]

        public string ExistenciasRequeridas { get; set; }
    }
}