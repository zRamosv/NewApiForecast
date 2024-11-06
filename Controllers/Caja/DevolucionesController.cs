using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Services;
using Microsoft.AspNetCore.Mvc;

namespace NewApiForecast.Controllers.Caja
{
    [ApiController]
    [Route("api/ModuloVentas/[controller]")]
    public class Devoluciones : ControllerBase
    {
        private readonly IDevolucionesService _devolucionesService;
        public Devoluciones(IDevolucionesService devolucionesService)
        {
            _devolucionesService = devolucionesService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevolucionPorIdDePedido(int id)
        {
            var devoluciones = await _devolucionesService.GetDevolucion(id);
            return devoluciones != null ? Ok(devoluciones) : NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetDevolucionesDeClienteEnFecha([FromBody] BuscarDevolucionDTO buscarDevolucion)
        {
            var devoluciones = await _devolucionesService.GetDevolucionesDeClienteEnPeriodo(buscarDevolucion);
            return devoluciones != null ? Ok(devoluciones) : NotFound();
        }
    }
}