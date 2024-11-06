using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities
{
    public class Devolucion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID_Devolucion { get; set; }
        [ForeignKey("Pedido_ID")]
        public int Pedido_ID { get; set; }
        public Ventas Pedido { get; set; }
        [ForeignKey("ID_Cliente")]
        public int ID_Cliente { get; set; }
        public Clientes Cliente { get; set; }
        public decimal Importe { get; set; }
        public DateTime Devuelto { get; set; }
        public string Moneda { get; set; }
        public decimal TipoDeCambio { get; set; }
    }
}
