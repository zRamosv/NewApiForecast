
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities
{
    public class Ventas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Venta_Id { get; set; }
        public int Product_Id { get; set; }
        public Productos Productos { get; set; }
        public DateOnly Fecha { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        public int Client_id { get; set; }
        public Clientes Cliente { get; set; }
        public bool MonedaUSD { get; set; }
        public int Vendor_Id { get; set; }
        public Vendedores Vendedores { get; set; }
    }
}