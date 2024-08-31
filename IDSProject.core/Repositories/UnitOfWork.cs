using IDSProject.core.Repositories;
using IDSProject.core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DemoProject.Core.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        // Add other repository interfaces as needed

        Task<int> CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private UserRepository? _userRepository;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IUserRepository Users =>
            (IUserRepository)(_userRepository ??= new UserRepository(_context));

        // Add other repositories as needed

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
