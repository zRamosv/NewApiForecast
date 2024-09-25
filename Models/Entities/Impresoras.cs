using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{


    public class Impresoras{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Printer_id {get;set;}
        public string TipoDocumento {get;set;}
        public string Impresora {get;set;}
        public int Sucursal_id {get;set;}
        public Sucursales Sucursales {get;set;}
    }
}