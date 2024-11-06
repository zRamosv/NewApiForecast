using NewApiForecast.Models.Entities.VentasModulo;

namespace NewApiForecast.Services.Ventas
{
    public interface ICotizacionesService
    {
        Task<List<Cotizacion>> GetCotizacionesAsync();
        Task<Cotizacion?> GetCotizacionByIdAsync(int id);
        Task<Cotizacion> CreateCotizacion(Cotizacion cotizacion);
        Task<Cotizacion> UpdateCotizacion(int id, Cotizacion cotizacion);
        
    }
}