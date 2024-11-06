using ApiForecast.Models.Entities;

namespace NewApiForecast.Services.VentasModulo
{
    public class CreateCotizacionDTO
    {

        public int Id_Cliente { get; set; }
        public Clientes Cliente { get; set; }  

        public int Id_Producto { get; set; }
        public Productos Producto { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public decimal  Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
    }
}