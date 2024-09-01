using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using IDSProject.core.Repositories.IRepositories;

namespace IDSProject.core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseServerContext _context;

        public UserRepository(DatabaseServerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _context.Set<User>().SingleOrDefaultAsync(u => u.Username == username);
            }
            catch (InvalidOperationException ex)
            {
                // Handle multiple records found
                // You can log this or handle it as appropriate for your application
                throw new Exception("Multiple users found with the same username. Please ensure usernames are unique.", ex);
            }
        }


        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _context.Set<User>().Where(u => u.Role == role).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByGenderAsync(string gender)
        {
            return await _context.Set<User>().Where(u => u.Gender == gender).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByAgeRangeAsync(int minAge, int maxAge)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return await _context.Set<User>().Where(u =>
                u.DateOfBirth.HasValue &&
                (today.Year - u.DateOfBirth.Value.Year) >= minAge &&
                (today.Year - u.DateOfBirth.Value.Year) <= maxAge
            ).ToListAsync();
        }

        public async Task UpdateUserPasswordAsync(int userId, string newPassword)
        {
            var user = await _context.Set<User>().FindAsync(userId);
            if (user != null)
            {
                user.Password = newPassword;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            var user = await _context.Set<User>().FindAsync(userId);
            if (user != null)
            {
                _context.Set<User>().Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddOrUpdateUserAsync(User user)
        {
            if (user.Id == 0)
            {
                // Add new user
                await _context.Set<User>().AddAsync(user);
            }
            else
            {
                // Update existing user
                var existingUser = await _context.Set<User>().FindAsync(user.Id);
                if (existingUser != null)
                {
                    _context.Entry(existingUser).CurrentValues.SetValues(user);
                }
                else
                {
                    throw new InvalidOperationException("User not found.");
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
