namespace ApiForecast.Models.DTOs
{
    public class DetalleCompraDTO
    {
        public int? Id_detalle { get; set; }
        public int? Id_orden { get; set; }
        public int? Id_producto { get; set; }

        public int? Cantidad { get; set; }
        public DateTime? Fecha_embarque { get; set; }
        public DateTime? Fecha_aduana { get; set; }
        public DateTime? Fecha_llegada_destino { get; set; }
        public string? Estado { get; set; }
    }
}
