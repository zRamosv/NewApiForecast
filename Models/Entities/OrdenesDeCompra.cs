using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{

    public class OrdenesDeCompra{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_orden {get; set;}
        public DateTime Fecha_solicitud {get; set;}
        public string Estado {get; set;}
        [JsonIgnore]
        public ICollection<DetallesOrdenCompra> Detalles_orden_compra {get; set;}   
    }
}