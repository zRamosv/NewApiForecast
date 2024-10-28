using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities
{

    public class ParametrosConfiguracion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Parametro { get; set; }
        public string Nombre_parametro { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Valor { get; set; }
        public string Descripcion { get; set; }
    }
}