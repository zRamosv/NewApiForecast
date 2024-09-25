using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{
    public class Puntos{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Points_id {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal PesosPorPunto{get; set;}
        public int PuntosMinimos {get; set;}
        public int Sucursal_id {get; set;}
        public Sucursales Sucursales {get; set;}
    }
    }
