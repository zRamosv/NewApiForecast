using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.InsertModels
{
    public class VendedoresInsert
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Comision { get; set; }
    }
}