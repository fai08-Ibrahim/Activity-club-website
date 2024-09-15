using Microsoft.EntityFrameworkCore;
using IDSProject.Models;
using IDSProject.core.Models;
using IDSProject.core.Repositories.IRepositories;

namespace IDSProject.core.Repositories
{
    public class EventGuideRepository : IEventGuideRepository
    {
        private readonly DatabaseServerContext _context;

        public EventGuideRepository(DatabaseServerContext context)
        {
            _context = context;
        }

        // Create a new EventGuide entry
        public async Task AddEventGuideAsync(EventGuide eventGuide)
        {
            var guideExists = await _context.Set<Guide>().AnyAsync(g => g.Id == eventGuide.GuideId);
            if (!guideExists)
            {
                throw new InvalidOperationException("Guide does not exist.");
            }

            bool eventExists = await _context.Set<Event>()
                .AnyAsync(e => e.Id == eventGuide.EventId && e.Name == eventGuide.EventName);
            if (!eventExists)
            {
                throw new InvalidOperationException("Event does not exist for this name and id combination.");
            }

            var existingAssignment = await _context.Set<EventGuide>()
                .FirstOrDefaultAsync(eg => eg.GuideId == eventGuide.GuideId && eg.EventId == eventGuide.EventId);

            if (existingAssignment != null)
            {
                throw new InvalidOperationException("This guide is already assigned to the event.");
            }

            await _context.Set<EventGuide>().AddAsync(eventGuide);
            await _context.SaveChangesAsync();
        }

        // Delete an EventGuide by ID
        public async Task DeleteEventGuideByIdAsync(int eventGuideId)
        {
            var eventGuide = await _context.Set<EventGuide>().FindAsync(eventGuideId);
            if (eventGuide != null)
            {
                _context.Set<EventGuide>().Remove(eventGuide);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EventGuide>> GetAllByGuideIdAsync(int guideId)
        {
            return await _context.Set<EventGuide>()
                .Where(eg => eg.GuideId == guideId)
                 .ToListAsync();
        }

        public async Task<IEnumerable<EventGuide>> GetAllByEventIdAsync(int eventId)
        {
            return await _context.Set<EventGuide>()
                .Where(eg => eg.EventId == eventId)
                .ToListAsync();
        }

        public async Task<EventGuide?> DeleteByGuideIdAndEventNameAsync(int guideId, string eventName)
        {
            var eventGuide = await _context.EventGuides
                .Include(eg => eg.Event)
                .FirstOrDefaultAsync(eg => eg.GuideId == guideId && eg.EventName == eventName);

            if (eventGuide != null)
            {
                _context.EventGuides.Remove(eventGuide);
                await _context.SaveChangesAsync();
                return eventGuide;
            }
            return null;
        }

        public async Task<IEnumerable<EventGuide>> DeleteAllByEventNameAsync(string eventName)
        {
            var eventGuides = await _context.EventGuides
                .Include(eg => eg.Event)
                .Where(eg => eg.EventName == eventName)
                .ToListAsync();

            if (eventGuides.Any())
            {
                _context.EventGuides.RemoveRange(eventGuides);
                await _context.SaveChangesAsync();
                return eventGuides;
            }
            return Enumerable.Empty<EventGuide>();
        }
    }
}