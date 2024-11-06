using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Models.InsertModels;
using ApiForecast.Services.Caja;
using Microsoft.AspNetCore.Mvc;
using NewApiForecast.Models.DTOs.VentasModulo;

namespace NewApiForecast.Controllers.Caja
{
    [Route("api/ModuloVentas/Caja")]
    [ApiController]
    public class CajaController : ControllerBase
    {
        private readonly ICajaService _cajaService;
        public CajaController(ICajaService cajaService)
        {
            _cajaService = cajaService;

        }

    
        [HttpPost]
        public async Task<IActionResult> PostVenta([FromBody] VentasInsert createVenta)
        {
            var venta = await _cajaService.VenderACliente(createVenta);
            return venta != null ? Ok(venta) : NotFound();
        }
        
    }
}