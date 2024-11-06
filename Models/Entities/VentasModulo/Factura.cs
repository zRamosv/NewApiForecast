
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NewApiForecast.Models.Entities.VentasModulo
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Factura { get; set; }
        [ForeignKey("Id_Pedido")]
        public int Id_Pedido { get; set; }
        public Pedido Pedido { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Moneda { get; set; }
        public decimal TipoDeCambio { get; set; }
        public string Estado { get; set; }
    }
}