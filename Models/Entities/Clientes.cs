using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NewApiForecast.Models.Entities;
using NewApiForecast.Models.Entities.VentasModulo;


namespace ApiForecast.Models.Entities
{


    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Client_id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        [JsonIgnore]
        public List<Pedido> Pedidos { get; set; }

        [JsonIgnore]
        public ICollection<Ventas> Ventas { get; set; }
        [JsonIgnore]
        public ICollection<CuentasPorCobrar> CuentasPorCobrar { get; set; }
    }
}