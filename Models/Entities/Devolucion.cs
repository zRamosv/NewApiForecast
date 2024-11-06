namespace ApiForecast.Models.Entities
{
    public class Devolucion
    {
        public int ID_Devolucion { get; set; }
        public int Pedido_ID { get; set; }
        public Ventas Pedido { get; set; }
        public int ID_Cliente { get; set; }
        public Clientes Cliente { get; set; }
        public decimal Importe { get; set; }
        public DateTime Devuelto { get; set; }
        public string Moneda { get; set; }
        public decimal TipoDeCambio { get; set; }
    }
}
