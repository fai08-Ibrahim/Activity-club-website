using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IDSProject.core.Repositories.IRepositories;
using IDSProject.Models;
using IDSProject.core.Models;

namespace IDSProject.core.Repositories
{
    public class EventMemberRepository : IEventMemberRepository
    {
        private readonly DatabaseServerContext _context;

        public EventMemberRepository(DatabaseServerContext context)
        {
            _context = context;
        }

        public async Task AddEventMemberAsync(EventMember eventMember)
        {
            // Validate existence of member
            if (!await _context.Members.AnyAsync(m => m.Id == eventMember.MemberId))
            {
                throw new InvalidOperationException("Member does not exist.");
            }

            // Validate existence of event with the specific name
            if (!await _context.Events.AnyAsync(e => e.Id == eventMember.EventId && e.Name == eventMember.EventName))
            {
                throw new InvalidOperationException("No event found with the provided ID and name combination.");
            }

            // Check for existing enrollment
            if (await _context.EventMembers.AnyAsync(em => em.MemberId == eventMember.MemberId && em.EventId == eventMember.EventId))
            {
                throw new InvalidOperationException("This member is already enrolled in the event.");
            }

            // Add new EventMember
            _context.EventMembers.Add(eventMember);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventMemberByIdAsync(int eventMemberId)
        {
            var eventMember = await _context.EventMembers.FindAsync(eventMemberId);
            if (eventMember == null)
            {
                throw new InvalidOperationException("Event member not found.");
            }
            _context.EventMembers.Remove(eventMember);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventMember>> GetAllByMemberIdAsync(int memberId)
        {
            return await _context.EventMembers.Where(em => em.MemberId == memberId).ToListAsync();
        }

        public async Task<IEnumerable<EventMember>> GetAllByEventIdAsync(int eventId)
        {
            return await _context.EventMembers.Where(em => em.EventId == eventId).ToListAsync();
        }

        public async Task<EventMember?> DeleteByMemberIdAndEventNameAsync(int memberId, string eventName)
        {
            var eventMember = await _context.EventMembers
                                            .Include(em => em.Event)
                                            .FirstOrDefaultAsync(em => em.MemberId == memberId && em.Event.Name == eventName);
            if (eventMember == null)
            {
                throw new InvalidOperationException("No event member found matching the criteria.");
            }
            _context.EventMembers.Remove(eventMember);
            await _context.SaveChangesAsync();
            return eventMember;
        }

        public async Task<IEnumerable<EventMember>> DeleteAllByEventNameAsync(string eventName)
        {
            var eventMembers = await _context.EventMembers
                                             .Include(em => em.Event)
                                             .Where(em => em.Event.Name == eventName)
                                             .ToListAsync();
            _context.EventMembers.RemoveRange(eventMembers);
            await _context.SaveChangesAsync();
            return eventMembers;
        }
    }
}
