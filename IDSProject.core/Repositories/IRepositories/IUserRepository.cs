using System.Collections.Generic;
using System.Threading.Tasks;
using DemoAPI.Models;

namespace IDSProject.core.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
        Task<IEnumerable<User>> GetUsersByGenderAsync(string gender);
        Task<IEnumerable<User>> GetUsersByAgeRangeAsync(int minAge, int maxAge);
        Task AddOrUpdateUserAsync(User user);
        Task DeleteUserByIdAsync(int userId);
    }
}
