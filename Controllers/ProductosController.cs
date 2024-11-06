using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Mvc;
using NewApiForecast.Services;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ProductosController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductosController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _productService.GetAllProductos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _productService.GetProducto(id);
            return producto != null ? Ok(producto) : NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Post(ProductosInsert producto)
        {
            Productos insert = await _productService.CreateProduct(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = insert.Product_Id }, insert);
        }

        

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, ProductosDTO producto)
        {
            var update = await _productService.UpdateProduct(id, producto);
            return Ok(update);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _productService.Delete(id);
            return NoContent();
        }


    }
}