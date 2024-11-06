using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewApiForecast.Models.Entities.VentasModulo
{
    public class ConsignacionPedido
    {
        [Key]
        public int Id_Consignacion { get; set; }
        [ForeignKey("Id_Pedido")]
        public int Id_Pedido { get; set; }
        public Pedido Pedido { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalConsignado { get; set; }
        public decimal MontoCobrado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Moneda { get; set; }

    }
}