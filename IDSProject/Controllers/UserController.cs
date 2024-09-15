using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IDSProject.Models;
using IDSProject.services.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IDSProject.core.Dtos;

namespace IDSProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("Get User by username")]
        public async Task<ActionResult<UserDto>> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet("Get All Users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpPost("Add Or Update User")]
        public async Task<IActionResult> AddOrUpdateUser(UserDto userDto)
        {
            if (userDto.Id == 0)
            {
                // New user
                var existingUser = await _userService.GetUserByUsernameAsync(userDto.Username);
                if (existingUser != null)
                {
                    // Username already exists
                    return Conflict("A user with this username already exists.");
                }
                // Map UserDto to User entity
                var user = _mapper.Map<User>(userDto);

                await _userService.AddOrUpdateUserAsync(user);
                return Ok("User was added successfully");
            }
            else
            {
                // Existing user
                var existingUser = await _userService.GetByUserIdAsync(userDto.Id);
                if (existingUser == null)
                {
                    // User ID does not exist
                    return NotFound("User you're trying to update does not exist.");
                }

                // Optionally check if the username is being changed and if the new username is unique
                if (existingUser.Username != userDto.Username)
                {
                    var usernameExists = await _userService.GetUserByUsernameAsync(userDto.Username);
                    if (usernameExists != null)
                    {
                        // New username already exists
                        return Conflict("A user with this username already exists.");
                    }
                }
                // Map UserDto to User entity
                var user = _mapper.Map<User>(userDto);

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
