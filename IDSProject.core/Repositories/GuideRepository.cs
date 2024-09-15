using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IDSProject.core.Repositories.IRepositories;
using IDSProject.Models;
using IDSProject.core.Models;

namespace IDSProject.core.Repositories
{
    public class GuideRepository : IGuideRepository
    {
        private readonly DatabaseServerContext _context;
        public GuideRepository(DatabaseServerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Guide>> GetAllGuides()
        {
            return await _context.Set<Guide>()
                .Include(g => g.EventGuides)  // Include the EventGuide relationship
                .ThenInclude(eg => eg.Event)  // Then include the Event details
                .ToListAsync();
        }

        public async Task<Guide?> GetByIdAsync(int id)
        {
            return await _context.Set<Guide>()
                .Include(g => g.EventGuides)  // Include the EventGuide relationship
                .ThenInclude(eg => eg.Event)  // Then include the Event details
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddOrUpdateGuideAsync(Guide guide)
        {
            // Check if the username is already used by another guide
            var existingGuideWithUsername = await _context.Set<Guide>()
                .FirstOrDefaultAsync(g => g.Username == guide.Username && g.Id != guide.Id);

            if (existingGuideWithUsername != null)
            {
                throw new InvalidOperationException("A guide with the same username already exists.");
            }

            if (guide.Id == 0)
            {
                // Add a new guide
                await _context.Set<Guide>().AddAsync(guide);
            }
            else
            {
                // Find the existing guide by Id
                var existingGuide = await _context.Set<Guide>().FindAsync(guide.Id);
                if (existingGuide != null)
                {
                    // Update the existing guide's details
                    _context.Entry(existingGuide).CurrentValues.SetValues(guide);
                }
                else
                {
                    throw new InvalidOperationException("Guide not found.");
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGuideByIdAsync(int guideId)
        {
            // Find the guide
            var guide = await _context.Set<Guide>().FindAsync(guideId);

            if (guide != null)
            {
                // Find all EventGuide entries related to this guide
                var eventGuides = await _context.Set<EventGuide>()
                    .Where(eg => eg.GuideId == guideId)
                    .ToListAsync();

                // Remove all related EventGuide entries
                if (eventGuides.Any())
                {
                    _context.Set<EventGuide>().RemoveRange(eventGuides);
                }

                // Remove the guide
                _context.Set<Guide>().Remove(guide);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Guide?> GetGuideByUsernameAsync(string username)
        {
            return await _context.Set<Guide>().SingleOrDefaultAsync(g => g.Username == username);
        }
    }
}
