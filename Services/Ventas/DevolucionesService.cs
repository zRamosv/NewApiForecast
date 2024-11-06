
using ApiForecast.Data;
using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Models.Entities;
using ApiForecast.Services;
using Microsoft.EntityFrameworkCore;

namespace NewApiForecast.Services.VentasModulo
{
    public class DevolucionesService : IDevolucionesService
    {
        private readonly ForecastContext _context;
        public DevolucionesService(ForecastContext context)
        {
            _context = context;
        }
        public async Task<Devolucion> CreateDevolucion(Devolucion devolucion)
        {
            _context.Devoluciones.Add(devolucion);
            await _context.SaveChangesAsync();
            return devolucion;
        }

        public async Task<Devolucion> GetDevolucion(int id)
        {
            return await _context.Devoluciones.FindAsync(id);
        }

        public async Task<List<Devolucion>> GetDevolucionesDeClienteEnPeriodo(BuscarDevolucionDTO buscarDevolucion)
        {
            var devolucionesDeCliente = await GetDevolucionsPorCliente(buscarDevolucion.Cliente_id);
            if (devolucionesDeCliente == null)
            {
                throw new Exception("No se encontraron devoluciones de ese cliente");
            }
            List<Devolucion> filtradas = devolucionesDeCliente
                .Where(todasAquellasQue =>
                {
                    bool DevueltoEntreFechaDeInicio = todasAquellasQue.Devuelto >= buscarDevolucion.Periodo_Inicio;
                    bool AntesDeFechaDeFin = todasAquellasQue.Devuelto <= buscarDevolucion.Periodo_Fin;
                    return DevueltoEntreFechaDeInicio && AntesDeFechaDeFin;
                })
                .ToList();
            return filtradas;
        }

        public async Task<List<Devolucion>> GetDevolucionsPorCliente(int Id_Cliente)
        {
            return await _context.Devoluciones.Where(such => such.Cliente.Client_id == Id_Cliente).ToListAsync();
        }
    }
}