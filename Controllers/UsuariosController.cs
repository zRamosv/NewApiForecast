using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.GetModels;
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

            return Ok(await _context.Usuarios.Include(x => x.UserRoles).ThenInclude(x => x.Role).Select(x => new GetUserRoles
            {
                User_Id = x.User_Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Foto_User = x.Foto_User,
                Telefono_Usuario = x.Telefono_Usuario,
                Contraseña = x.Contraseña,
                Estatus = x.Estatus,
                Vendedor_Modo = x.Vendedor_Modo,
                VendedorComision = x.VendedorComision,
                VendedorClave = x.VendedorClave,
                UserRoles = x.UserRoles.Select(y => new UserRolesDTO
                {
                    Id = y.Id,
                    User_Id = y.User_Id,
                    Role_Id = y.Role_Id,
                    Role = y.Role,
                    Sucursal_id = y.Sucursal_id,
                    Sucursal = y.Sucursal
                }).ToList()
            }).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.Include(x => x.UserRoles).ThenInclude(x => x.Role).Select(x => new GetUserRoles
            {
                User_Id = x.User_Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Foto_User = x.Foto_User,
                Telefono_Usuario = x.Telefono_Usuario,
                Contraseña = x.Contraseña,
                Estatus = x.Estatus,
                Vendedor_Modo = x.Vendedor_Modo,
                VendedorComision = x.VendedorComision,
                VendedorClave = x.VendedorClave,
                UserRoles = x.UserRoles.Select(y => new UserRolesDTO
                {
                    Id = y.Id,
                    User_Id = y.User_Id,
                    Role_Id = y.Role_Id,
                    Role = y.Role,
                    Sucursal_id = y.Sucursal_id,
                    Sucursal = y.Sucursal
                }).ToList()
            }).FirstOrDefaultAsync(x => x.User_Id == id);

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
        public async Task<IActionResult> SubirFoto(int id, IFormFile file)
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
        [HttpPost("Roles")]
        public async Task<IActionResult> PostRoles([FromBody] InsertUserRoleBatch roles)
        {
            var userRole = await _context.User_Roles.Where(x => x.User_Id == roles.IdUsuario).ToListAsync();
            if (userRole != null)
            {
                _context.User_Roles.RemoveRange(userRole);
                await _context.SaveChangesAsync();
            }
            foreach (var item in roles.Roles)
            {
                var insert = new UserRoles
                {
                    User_Id = roles.IdUsuario,
                    Role_Id = item.RolId,
                    Sucursal_id = item.SucursalId
                };
                await _context.User_Roles.AddAsync(insert);
            }
            await _context.SaveChangesAsync();
            return Ok(roles);
        }
        [HttpGet("Roles/{userId}")]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var roles = await _context.User_Roles.Where(x => x.User_Id == userId).ToListAsync();
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
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
                if (property.Name == nameof(update.Contraseña) && usuario.Contraseña != null)
                {
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