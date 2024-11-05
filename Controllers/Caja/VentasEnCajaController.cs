using ApiForecast.Models.InsertModels;
using ApiForecast.Services.Caja;
using Microsoft.AspNetCore.Mvc;
using NewApiForecast.Models.DTOs.VentasModulo;

namespace NewApiForecast.Controllers.Caja
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasEnCajaController : ControllerBase
    {
        private readonly ICajaService _cajaService;
        public VentasEnCajaController(ICajaService cajaService)
        {
            _cajaService = cajaService;

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenta(int id, [FromBody] BuscarFacturaDTO buscarFactura)
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