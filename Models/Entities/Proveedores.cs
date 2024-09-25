using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiForescast.Models.Entities;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{

    public class Proveedores{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Provider_id { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        [JsonIgnore]
        public ICollection<Compras> Compras { get; set; }
    }
}