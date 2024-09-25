using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{

    public class Reportes{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Report_id { get; set; }
        public string Tipo { get; set; }
        public DateOnly Fecha_inicio { get; set; }
        public DateOnly Fecha_fin { get; set; }
        public string Detalles { get; set; }
    }
}