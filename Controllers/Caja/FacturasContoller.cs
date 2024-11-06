using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Services.Caja;
using Microsoft.AspNetCore.Mvc;
using NewApiForecast.Models.DTOs.VentasModulo;

namespace NewApiForecast.Controllers.Caja
{
    [ApiController]
    [Route("api/ModuloVentas")]
    public class FacturasContoller : ControllerBase
    {
        private readonly ICajaService _cajaService;
        public FacturasContoller(ICajaService cajaService)
        {
            _cajaService = cajaService;
        }
        
        [Route("BuscarFactura")]
        [HttpPost]
        public async Task<IActionResult> BuscarFactura([FromBody] BuscarFacturaDTO buscarFactura)
        {
            var venta = await _cajaService.BuscarFactura(buscarFactura);
            return venta != null ? Ok(venta) : NotFound();
        }
    }
}