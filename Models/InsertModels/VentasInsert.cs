using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.InsertModels
{
    public class VentasInsert
    {
        [Required]
        public int Product_Id { get; set; }
        [Required]

        public DateTime Fecha { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Required]
        public int Client_id { get; set; }
        [Required]
        public bool MonedaUSD { get; set; }
        [Required]
        public int Vendor_Id { get; set; }
    }
}