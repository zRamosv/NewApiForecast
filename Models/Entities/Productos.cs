using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiForescast.Models.Entities;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities
{


    public class Productos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int Group_Id { get; set; }
        public int Provider_id { get; set; }
        public Grupos Grupos { get; set; }
        [JsonIgnore]

        public ICollection<Compras> Compras { get; set; }
        [JsonIgnore]
        public ICollection<Ventas> Ventas { get; set; }
        [JsonIgnore]
        public ICollection<Forecast> Forecasts { get; set; }
        [JsonIgnore]
        public ICollection<DetalleForecast> DetalleForecast { get; set; }
        [JsonIgnore]
        public ICollection<DetallesOrdenCompra> DetallesOrdenCompra { get; set; }
        [JsonInclude]
        public Proveedores Proveedor { get; set; }
    }
}