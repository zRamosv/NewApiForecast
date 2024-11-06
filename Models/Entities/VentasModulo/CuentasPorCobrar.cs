using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Models.Entities;

namespace NewApiForecast.Models.Entities.VentasModulo
{
    public class CuentasPorCobrar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Cuenta { get; set; }
        [ForeignKey("Id_Cliente")]
        public int Id_Cliente { get; set; }
        public Clientes Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public decimal TipoDeCambio { get; set; }
        public string Estado { get; set; }
    }
}