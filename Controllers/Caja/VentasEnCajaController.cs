using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Models.InsertModels;
using ApiForecast.Services.Caja;
using Microsoft.AspNetCore.Mvc;
using NewApiForecast.Models.DTOs.VentasModulo;

namespace NewApiForecast.Controllers.Caja
{
    [Route("api/ModuloVentas/[controller]")]
    [ApiController]
    public class CajaController : ControllerBase
    {
        private readonly ICajaService _cajaService;
        public CajaController(ICajaService cajaService)
        {
            _cajaService = cajaService;

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactura(int id, [FromBody] BuscarFacturaDTO buscarFactura)
        {
            var venta = await _cajaService.VerFactura(id, buscarFactura);
            return venta != null ? Ok(venta) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostVenta([FromBody] VentasInsert createVenta)
        {
            var venta = await _cajaService.VenderACliente(createVenta);
            return venta != null ? Ok(venta) : NotFound();
        }
        
    }
}