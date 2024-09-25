using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.InsertModels
{
    public class PuntosInsert
    {
        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PesosPorPunto { get; set; }
        [Required]
        public int PuntosMinimos { get; set; }
        [Required]
        public int Sucursal_id { get; set; }
    }
}