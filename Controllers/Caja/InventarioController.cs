using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Services.Caja;
using Microsoft.AspNetCore.Mvc;

namespace NewApiForecast.Services.Caja
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private IInventarioService _inventarioService;

        public InventarioController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventario()
        {
            var productos = await _inventarioService.GetFullInventario();
            return Ok(productos);
        }

        [HttpPost("ComprarDeProveedores")]
        public async Task<IActionResult> ComprarDeProveedores([FromBody] OrdenesDeCompra orden)
        {
            var productos = await _inventarioService.ComprarDeProveedores(orden);
            return Ok(productos);
        }

        [HttpPost("VenderAClientes")]
        public async Task<IActionResult> VenderAClientes([FromBody] Clientes cliente)
        {
            var productos = await _inventarioService.VenderAClientes(cliente);
            return Ok(productos);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateInventario([FromBody] ProductosDTO nuevoStock)
        {
            var producto = await _inventarioService.UpdateInventario(nuevoStock);
            return Ok(producto);
        }
    }
}