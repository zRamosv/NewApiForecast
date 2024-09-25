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
    public class UserRolesController : ControllerBase
    {
        private readonly ForecastContext _context;
        public UserRolesController(ForecastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.User_Roles.Include(x => x.User).Include(x => x.Role).Include(x => x.Sucursal).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRole(int id)
        {
            var user_Roles = await _context.User_Roles.Include(x => x.User).Include(x => x.Role).Include(x => x.Sucursal).FirstOrDefaultAsync(x => x.Id == id);
            if (user_Roles == null){
                return NotFound();
            }
            return Ok(user_Roles);
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserRoleInsert user_Roles)
        {
            var insert = new UserRoles
            {
                User_Id = user_Roles.User_Id,
                Role_Id = user_Roles.Role_Id,
                Sucursal_id = user_Roles.Sucursal_id
            };
            _context.User_Roles.Add(insert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserRole), new { id = insert.Id }, insert);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserRolesDTO user_Roles)
        {
            var update = await _context.User_Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (update == null)
            {
                return NotFound();
            }
            update.User_Id = user_Roles.User_Id ?? update.User_Id;
            update.Role_Id = user_Roles.Role_Id ?? update.Role_Id;
            update.Sucursal_id = user_Roles.Sucursal_id ?? update.Sucursal_id;
            await _context.SaveChangesAsync();
            return Ok(update);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.User_Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.User_Roles.Remove(delete);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
