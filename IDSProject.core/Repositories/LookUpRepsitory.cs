using IDSProject.core.Models;
using IDSProject.Models;
using IDSProject.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDSProject.core.Repositories
{
    public class LookUpRepository : ILookUpRepository
    {
        private readonly DatabaseServerContext _context;

        public LookUpRepository(DatabaseServerContext context)
        {
            _context = context;
        }

        // Get all LookUps
        public async Task<IEnumerable<LookUp>> GetAllLookUpsAsync()
        {
            return await _context.Set<LookUp>().ToListAsync();
        }

        // Get LookUp by Code
        public async Task<LookUp?> GetLookUpByCodeAsync(int code)
        {
            return await _context.Set<LookUp>().FindAsync(code);
        }

        // Add a new LookUp
        public async Task AddLookUpAsync(LookUp lookUp)
        {
            await _context.Set<LookUp>().AddAsync(lookUp);
            await _context.SaveChangesAsync();
        }

        // Update an existing LookUp
        public async Task UpdateLookUpAsync(LookUp lookUp)
        {
            var existingLookUp = await _context.Set<LookUp>().FindAsync(lookUp.Code);
            if (existingLookUp != null)
            {
                _context.Entry(existingLookUp).CurrentValues.SetValues(lookUp);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("LookUp not found.");
            }
        }

        // Delete LookUp by Code
        public async Task DeleteLookUpAsync(int code)
        {
            var lookUp = await _context.Set<LookUp>().FindAsync(code);
            if (lookUp != null)
            {
                _context.Set<LookUp>().Remove(lookUp);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("LookUp not found.");
            }
        }
    }
}
