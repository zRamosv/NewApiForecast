using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Data;
using ApiForecast.Models.Entities;
using NewApiForecast.Models.Entities.VentasModulo;

namespace NewApiForecast.Services.Ventas
{
    public class CotizacionesService : ICotizacionesService
    {
        private readonly ForecastContext _context;
        public CotizacionesService(ForecastContext context)
        {
            _context = context;
        }
        public Task<Cotizacion> CreateCotizacion(Cotizacion cotizacion)
        {
            throw new NotImplementedException();
        }

        public Task<Cotizacion?> GetCotizacionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cotizacion>> GetCotizacionesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cotizacion> UpdateCotizacion(int id, Cotizacion cotizacion)
        {
            throw new NotImplementedException();
        }
    }
}