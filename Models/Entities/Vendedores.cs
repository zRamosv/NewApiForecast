using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{

    public class Vendedores{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vendor_id { get; set; }
        public string Nombre { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Comision { get; set; }
        [JsonIgnore]

        public ICollection<Ventas> Ventas { get; set; }
    }
}