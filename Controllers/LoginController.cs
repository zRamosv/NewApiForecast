using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using ApiForecast.Data;
using ApiForecast.Models.Entities;
using ApiForecast.Models.DTOs;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ForecastContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(ForecastContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _context.Usuarios.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Clave == login.Username);
            if (user == null || !user.VerifyPassword(login.Password))
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(user);
            return Ok(new { token });


        }

        private string GenerateJwtToken(Usuarios user)
        {
            // Configura la clave de seguridad y las credenciales de firma para el token JWT
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Obtener el primer rol del usuario de la lista de UserRoles; si no hay roles, asigna "Usuario" por defecto
            var userRole = user.UserRoles.FirstOrDefault()?.Role.Nombre ?? "Usuario";

            // Crea los claims personalizados para incluir en el token JWT
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.User_Id.ToString()), // Identificador del usuario
            new Claim(ClaimTypes.Name, user.Nombre), // Nombre del usuario
            new Claim(ClaimTypes.Role, userRole) // Rol del usuario
        };

            // Agrega claims adicionales basados en los permisos asociados al usuario
            var userPermissions = _context.Permisos.Where(p => p.User_id == user.User_Id).ToList();
            foreach (var permiso in userPermissions)
            {
                // Agrega cada permiso como un claim personalizado
                claims.Add(new Claim("Permiso", $"{permiso.Modulo}:{permiso.Nivel_acceso}"));
            }
            claims.Add(new Claim("IdUsuario", user.User_Id.ToString()));

            // Configura y crea el token JWT con los claims y las credenciales de firma
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // Emisor del token
                audience: _configuration["Jwt:Audience"], // Audiencia del token
                claims: claims, // Claims a incluir en el token
                expires: DateTime.Now.AddMinutes(120), // Tiempo de expiración del token (120 minutos)
                signingCredentials: credentials); // Credenciales de firma

            // Genera y retorna el token JWT como una cadena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}