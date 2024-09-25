using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{


    public class Parametros{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Parameters_id {get; set;}
        public int Sucursal_id {get; set;}
        public Sucursales Sucursal {get; set;}
        public string FirmaSupervisor {get; set;}
        public string ExistenciasRequeridas {get; set;}
    }
}