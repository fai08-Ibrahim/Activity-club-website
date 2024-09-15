using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;

namespace IDSProject.services.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetByUserIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task AddOrUpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}
