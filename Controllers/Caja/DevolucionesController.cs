
using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Services;
using Microsoft.AspNetCore.Mvc;

namespace NewApiForecast.Controllers.Caja
{
    [ApiController]
    [Route("api/ModuloVentas/Devoluciones")]
    public class DevolucionesController : ControllerBase
    {
        private readonly IDevolucionesService _devolucionesService;
        public DevolucionesController(IDevolucionesService devolucionesService)
        {
            _devolucionesService = devolucionesService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevolucionPorIdDePedido(int id)
        {
            var devoluciones = await _devolucionesService.GetDevolucion(id);
            return devoluciones != null ? Ok(devoluciones) : NotFound();
        }
        
        [Route("BuscarDevolucion")]
        [HttpPost]
        public async Task<IActionResult> BuscarDevolucion([FromBody] BuscarDevolucionDTO buscarDevolucion)
        {
            var devolucion = await _devolucionesService.GetDevolucionesDeClienteEnPeriodo(buscarDevolucion);
            return devolucion != null ? Ok(devolucion) : NotFound();
        }
    }
}