
using ApiForecast.Models.Entities;
using NewApiForecast.Models.Entities;

namespace NewApiForecast.Models.DTOs.VentasModulo
{
    public class VerFacturaDTO
    {
        public Pedido Pedido { get; set; }
        public Clientes Cliente { get; set; }
        public DateOnly Fecha { get; set; }
        public string Estatus { get; set; }
        public decimal Importe { get; set; }

        public string Moneda { get; set; }

        public double TipoDeCambio { get; set; }

    }
}