namespace ApiForecast.Models.DTOs.CajaModulo
{
    public class BuscarDevolucionDTO
    {
        public int Cliente_id { get; set; }
        public DateTime Periodo_Inicio { get; set; }
        public DateTime Periodo_Fin { get; set; }
    }
}