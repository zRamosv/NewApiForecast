using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Models.Entities;

namespace NewApiForecast.Models.DTOs.VentasModulo
{
    public class VerFacturaDTO
    {
        public Ventas Pedido { get; set; }
        public Clientes cliente { get; set; }
        public DateOnly Fecha { get; set; }
        public string Estatus { get; set; }
        public decimal Importe { get; set; }

        public string Moneda { get; set; }

        public float TipoDeCambio { get; set; }

    }
}