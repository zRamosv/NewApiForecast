

using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Models.Entities;

namespace ApiForecast.Services
{
    public interface IDevolucionesService
    {
        Task<Devolucion> GetDevolucion(int id);
        Task<List<Devolucion>> GetDevolucionsPorCliente(int Id_Cliente);
        Task<Devolucion> CreateDevolucion(Devolucion devolucion);

        Task<List<Devolucion>> GetDevolucionesDeClienteEnPeriodo(BuscarDevolucionDTO buscarDevolucion);
    }
}