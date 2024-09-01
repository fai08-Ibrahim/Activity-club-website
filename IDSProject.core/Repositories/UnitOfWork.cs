using DemoAPI.Models;
using IDSProject.core.Repositories;
using IDSProject.core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DemoProject.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseServerContext _context; // Use your specific DbContext
        private UserRepository? _userRepository;

        public UnitOfWork(DatabaseServerContext context) // Update constructor to use DatabaseServerContext
        {
            _context = context;
        }

        public IUserRepository Users =>
            _userRepository ??= new UserRepository(_context);

        UserRepository IUnitOfWork.Users => throw new NotImplementedException();

        // Implement other repositories as needed

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
