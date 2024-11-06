using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using ApiForecast.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private IVentasService _ventasService;
        

        public VentasController(IVentasService ventasService )
        {
            _ventasService = ventasService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ventas = await _ventasService.GetVentasAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenta(int id)
        {
            var venta = await _ventasService.GetVentaByIdAsync(id);
            return venta != null ? Ok(venta) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Post(VentasInsert venta)
        {
            Ventas insert = await _ventasService.CreateVenta(venta);
            return CreatedAtAction(nameof(GetVenta), new { id = insert.Venta_Id }, insert);

            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VentasDTO venta)
        {
            var update = await _ventasService.UpdateVenta(id, venta);
            return update is not null ? Ok(update) : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _ventasService.DeleteVenta(id);
            return delete is null ? NotFound() : NoContent();
        }
    }
}