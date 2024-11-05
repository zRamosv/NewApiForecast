using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using NewApiForecast.Models.Enums;

namespace NewApiForecast.Models.Entities
{
    public class Pedido
    {
        [Key]
        public int Id_Pedido { get; set; }
        public FomaDePago FormaDePago { get; set; }
        public DateTime Periodo_Inicio { get; set; }
        public DateTime Periodo_Fin { get; set; }

        public float Iva_Aplicable { get; set; }
        public EstatusDePedido Estatus { get; set; }

        [ForeignKey("Id_Cotizacion")]
        public int Id_Cotizacion { get; set; }
        // TODO: public Cotizacion Cotizacion { get; set; }
    }
}