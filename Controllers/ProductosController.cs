using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ProductosController : ControllerBase
    {

        private readonly ForecastContext _context;
        public ProductosController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Productos.Include(x => x.Grupos).Include(x => x.Proveedor).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _context.Productos.Include(x => x.Grupos).Include(x => x.Proveedor).FirstOrDefaultAsync(x => x.Product_Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductosInsert producto)
        {
            var insert = new Productos
            {
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Clave = producto.Clave,
                Descripcion = producto.Descripcion,
                Group_Id = producto.Group_Id,
                Provider_id = producto.Provider_id
            };
            _context.Productos.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducto), new { id = insert.Product_Id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductosDTO producto)
        {
            var update = await _context.Productos.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = producto.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(producto);
                    if (newValue != null)
                    {
                        property.SetValue(update, newValue);
                    }
                }
            }
            await _context.SaveChangesAsync(); 
            return Ok(update);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.Productos.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}