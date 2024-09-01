using System.Collections.Generic;
using System.Threading.Tasks;
using DemoAPI.Models;
using IDSProject.core.Repositories;
using IDSProject.core.Repositories.IRepositories;

namespace IDSProject.services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByUserIdAsync(int userId) // Implement the method
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task AddOrUpdateUserAsync(User user)
        {
            try
            {
                await _userRepository.AddOrUpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                // Handle exceptions related to unique constraint
                throw new Exception("An error occurred while adding or updating the user. Please ensure usernames are unique.", ex);
            }
        }


        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserByIdAsync(userId);
        }
    }
}
