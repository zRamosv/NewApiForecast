using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiForescast.Models.Entities;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{

    public class Proveedores{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Provider_id { get; set; }

        [MaxLength(50)]
        public string Clave { get; set; }

        [MaxLength(500)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        public string Persona { get; set; }

        [MaxLength(255)]
        public string RFC { get; set; }

        [MaxLength(255)]
        public string CURP { get; set; }

        [MaxLength(255)]
        public string Empleado { get; set; }

        [MaxLength(255)]
        public string CalleYNumero { get; set; }

        [MaxLength(255)]
        public string Colonia { get; set; }

        [MaxLength(255)]
        public string CodigoPostal { get; set; }

        [MaxLength(255)]
        public string DelegacionMunicipio { get; set; }

        [MaxLength(255)]
        public string Ciudad { get; set; }

        [MaxLength(255)]
        public string Estado { get; set; }

        [MaxLength(255)]
        public string Telefonos { get; set; }

        [MaxLength(255)]
        public string Fax { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Contacto { get; set; }

        public int? CreditoDias { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? CreditoMonto { get; set; }

        [MaxLength(255)]
        public string Moneda { get; set; }

        [MaxLength(255)]
        public string ReferenciaBancaria { get; set; }

        public DateTime? UltimaCompra { get; set; }

        [MaxLength(255)]
        public string Estatus { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Observaciones { get; set; }
        [JsonIgnore]
        public ICollection<Compras> Compras { get; set; }
      
    }
}