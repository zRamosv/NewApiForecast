using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.InsertModels
{
    public class ComprasInsert
    {
        [Required]
        public int Product_id { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateOnly Fecha { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Required]
        public int Provider_id { get; set; }
        [Required]
        
        public bool MoendaUSD { get; set; }
    }
}