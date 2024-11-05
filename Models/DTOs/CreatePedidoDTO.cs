using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewApiForecast.Models.DTOs
{
    public class CreatePedidoDTO
    {
        public int Id_Pedido { get; set; }
        public string FormaDePago { get; set; }
        public DateTime Periodo_Inicio { get; set; }
        public DateTime Periodo_Fin { get; set; }
        public float Iva_Aplicable { get; set; }
        public string Estatus { get; set; }
        public int Id_Cotizacion { get; set; }
        // TODO: public Cotizacion Cotizacion { get; set; }
    }
}