using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{
    public class Sucursales{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sucursal_id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [JsonIgnore]
        public ICollection<AccessoSucursales> AccessoSucursales { get; set; }
        [JsonIgnore]
        public ICollection<Folios> Folios { get; set; }
        [JsonIgnore]
        public ICollection<Impresoras> Impresoras { get; set; }
        [JsonIgnore]
        public ICollection<Parametros> Parametros { get; set; }
        [JsonIgnore]
        public ICollection<Puntos> Puntos { get; set; }
        [JsonIgnore]
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}   