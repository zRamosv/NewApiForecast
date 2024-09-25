using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs
{

    public class ComprasDTO
    {

        public int? Purchase_id { get; set; }
        public int? Product_id { get; set; }
        public Productos? Product { get; set; }
        public DateOnly? Fecha { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public int? Provider_id { get; set; }
        public Proveedores? Proveedor { get; set; }
        public bool? MoendaUSD { get; set; }
    }
}