using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using ApiForecast.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

    public class ClientesController : ControllerBase
    {

        private readonly ForecastContext _context;

        public ClientesController(ForecastContext context)
        {
            _context = context;

        }

       
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {


            return Ok(await _context.Clientes.ToListAsync());

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] ClientesInsert cliente)
        {
            var insert = new Clientes
            {

                Nombre = cliente.Nombre,
                Direccion = cliente.Direccion,
                Contacto = cliente.Contacto,
            };

            _context.Clientes.Add(insert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = insert.Client_id }, insert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] ClientesDTO cliente)
        {
            var update = await _context.Clientes.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {

                if (property.Name == nameof(update.Client_id))
                {
                    continue;
                }

                var dtoProperty = cliente.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(cliente);
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
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}