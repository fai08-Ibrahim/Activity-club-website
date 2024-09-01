using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;
using IDSProject.services.Services;

namespace IDSProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Get User by username")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("Get All Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost("Add Or Update User")]
        public async Task<IActionResult> AddOrUpdateUser(User user)
        {
            if (user.Id == 0)
            {
                // New user
                var existingUser = await _userService.GetUserByUsernameAsync(user.Username);
                if (existingUser != null)
                {
                    // Username already exists
                    return Conflict("A user with this username already exists.");
                }

                await _userService.AddOrUpdateUserAsync(user);
                return Ok("User was added successfully");
            }
            else
            {
                // Existing user
                var existingUser = await _userService.GetByUserIdAsync(user.Id);
                if (existingUser == null)
                {
                    // User ID does not exist
                    return NotFound("User you're trying to update does not exist.");
                }

                // Optionally check if the username is being changed and if the new username is unique
                if (existingUser.Username != user.Username)
                {
                    var usernameExists = await _userService.GetUserByUsernameAsync(user.Username);
                    if (usernameExists != null)
                    {
                        // New username already exists
                        return Conflict("A user with this username already exists.");
                    }
                }

                await _userService.AddOrUpdateUserAsync(user);
                return Ok("User was updated successfully");
            }
        }



        [HttpDelete("Delete User by their userId")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            // Check if the user exists
            var user = await _userService.GetByUserIdAsync(userId);
            if (user == null)
            {
                // User does not exist
                return Ok($"This user with ID = {userId} does not exist.");
            }

            // Delete the user
            await _userService.DeleteUserAsync(userId);
            return Ok("User was deleted successfully.");
        }

    }
}
