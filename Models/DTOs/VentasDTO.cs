
using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs
{

    public class VentasDTO
    {
        public int? Venta_Id { get; set; }
        public int? Product_Id { get; set; }
        public Productos ?Productos { get; set; }
        public DateOnly? Fecha { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public int? Client_id { get; set; }
        public Clientes? Clientes { get; set; }
        public bool MonedaUSD { get; set; }
        public int Vendor_Id { get; set; }
        public Vendedores ?Vendedores { get; set; }
    }
}