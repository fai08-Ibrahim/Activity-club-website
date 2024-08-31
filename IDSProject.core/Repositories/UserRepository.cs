using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IDSProject.core.Repositories
{
    internal class UserRepository
    {
        private readonly DbContext Context;

        public UserRepository(DbContext context)
        {
            Context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await Context.Set<User>().SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await Context.Set<User>().Where(u => u.Role == role).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByGenderAsync(string gender)
        {
            return await Context.Set<User>().Where(u => u.Gender == gender).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByAgeRangeAsync(int minAge, int maxAge)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return await Context.Set<User>().Where(u =>
                u.DateOfBirth.HasValue &&
                (today.Year - u.DateOfBirth.Value.Year) >= minAge &&
                (today.Year - u.DateOfBirth.Value.Year) <= maxAge
            ).ToListAsync();
        }

        public async Task UpdateUserPasswordAsync(int userId, string newPassword)
        {
            var user = await Context.Set<User>().FindAsync(userId);
            if (user != null)
            {
                user.Password = newPassword;
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            var user = await Context.Set<User>().FindAsync(userId);
            if (user != null)
            {
                Context.Set<User>().Remove(user);
                await Context.SaveChangesAsync();
            }
        }
    }
}
