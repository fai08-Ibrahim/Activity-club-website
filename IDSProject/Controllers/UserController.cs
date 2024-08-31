using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseServerContext _context;
        public UserController(DatabaseServerContext context) 
        {
            _context = context;
        }
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, innerException = ex.InnerException?.Message });
            }
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser(User user)
        {
            try
            {
                if(user.Id == 0)
                {
                    var addUser = await _context.Users.AddAsync(user);
                }
                else
                {
                    var checkIfExist = await _context.Users.Where(u => u.Id == user.Id).SingleOrDefaultAsync();
                    if (checkIfExist != null)
                    {
                        var updateUser = _context.Users.Update(user);
                    }
                    else
                    {
                        return Ok("Not Found");
                    }
                }
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            catch(System.Exception ex)
            {
                return BadRequest(new { message = ex.Message, innerException = ex.InnerException?.Message });
            }
        }
    }
}
