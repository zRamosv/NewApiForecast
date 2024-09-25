using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs
{

    public class FoliosDTO
    {
        public int? Folio_id { get; set; }
        public string? TipoDocumento { get; set; }
        public int? SiguienteFolio { get; set; }
        public int? Sucursal_id { get; set; }
        public Sucursales? Sucursales { get; set; }

    }
}