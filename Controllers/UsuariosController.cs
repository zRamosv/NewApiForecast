using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using ApiForecast.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ForecastContext _context;
        private readonly UploadFile _uploadFile;
        public UsuariosController(ForecastContext context, UploadFile uploadFile)
        {
            _context = context;
            _uploadFile = uploadFile;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {

            return Ok(await _context.Usuarios.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioInsert usuario)
        {
            var insert = new Usuarios
            {
                Clave = usuario.Clave,
                Nombre = usuario.Nombre,
                Foto_User = string.Empty,
                Telefono_Usuario = usuario.TelefonoUsuario,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña),
                Estatus = usuario.Estatus,
                Vendedor_Modo = usuario.Vendedor_Modo,
                VendedorComision = usuario.VendedorComision,
                VendedorClave = usuario.VendedorClave
            };

            _context.Usuarios.Add(insert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = insert.User_Id }, insert);
        }
        [HttpPost("SubirFoto/{id}")]
        public async Task<IActionResult> SubirFoto(int id,  IFormFile file)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Foto_User = await _uploadFile.Upload(file);
            await _context.SaveChangesAsync();
            return Ok(usuario);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UsuarioDTO usuario)
        {
            var update = await _context.Usuarios.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            foreach (var property in update.GetType().GetProperties())
            {

                if (property.Name == nameof(update.User_Id))
                {
                    continue;
                }
                if(property.Name == nameof(update.Contraseña) && usuario.Contraseña != null){
                    update.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
                    continue;
                }

                var dtoProperty = usuario.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(usuario);
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
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}