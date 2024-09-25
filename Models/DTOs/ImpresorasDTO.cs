using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs{

    public class ImpresorasDTO{

         public int? Printer_id {get;set;}
        public string? TipoDocumento {get;set;}
        public string? Impresora {get;set;}
        public int? Sucursal_id {get;set;}
        public Sucursales? Sucursales {get;set;}
    }
}