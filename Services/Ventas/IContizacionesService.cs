using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewApiForecast.Models.Entities.VentasModulo;
using NewApiForecast.Services.VentasModulo;

namespace NewApiForecast.Services.Ventas
{
    public interface IContizacionesService
    {
        Task<List<Cotizacion>> GetVentasAsync();
        Task<Cotizacion?> GetVentaByIdAsync(int id);
        Task<Cotizacion> CreateVenta(CreateCotizacionDTO venta);

        Task<Cotizacion> UpdateVenta(int id, UpdateCotizacionDTO venta);

        Task<Cotizacion> DeleteVenta(int id);
    }
}