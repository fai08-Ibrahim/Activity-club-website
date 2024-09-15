using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSProject.Models;
using Microsoft.EntityFrameworkCore;
using IDSProject.core.Repositories.IRepositories;
using IDSProject.core.Models;

namespace IDSProject.core.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseServerContext _context;

        public EventRepository(DatabaseServerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Set<Event>()
                .Include(e => e.CategoryCodeNavigation)
                .ToListAsync();
        }
        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Set<Event>()
                .Include(e => e.CategoryCodeNavigation)
                .FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task AddOrUpdateEventAsync(Event @event)
        {
            try
            {
                // Check if the event name is already used by another event
                var existingEventWithName = await _context.Set<Event>()
                    .FirstOrDefaultAsync(e => e.Name == @event.Name && e.Id != @event.Id);

                if (existingEventWithName != null)
                {
                    throw new InvalidOperationException("An event with the same name already exists.");
                }

                // Check and handle CategoryCodeNavigation (LookUp)
                if (@event.CategoryCodeNavigation != null)
                {
                    // If Code == 0, check for the name and add a new LookUp entry if the name doesn't exist
                    if (@event.CategoryCodeNavigation.Code == 0)
                    {
                        var existingLookUpWithName = await _context.Set<LookUp>()
                            .FirstOrDefaultAsync(l => l.Name == @event.CategoryCodeNavigation.Name);

                        if (existingLookUpWithName != null)
                        {
                            throw new InvalidOperationException("A LookUp with the same name already exists.");
                        }

                        // Add a new LookUp entry since Code is 0 and no existing entry with the same name
                        await _context.Set<LookUp>().AddAsync(@event.CategoryCodeNavigation);
                        await _context.SaveChangesAsync(); // Save the LookUp entry first to generate the Code

                        // Assign the generated Code to the Event
                        @event.CategoryCode = @event.CategoryCodeNavigation.Code;
                    }
                    else
                    {
                        // If Code != 0, check if a LookUp entry with the same Code exists
                        var existingLookUpWithCode = await _context.Set<LookUp>()
                            .FirstOrDefaultAsync(l => l.Code == @event.CategoryCodeNavigation.Code);

                        if (existingLookUpWithCode == null)
                        {
                            throw new InvalidOperationException("No LookUp entry found with the provided Code.");
                        }

                        // If a LookUp with the same Code exists but the names differ, update the LookUp
                        if (existingLookUpWithCode.Name != @event.CategoryCodeNavigation.Name)
                        {
                            existingLookUpWithCode.Name = @event.CategoryCodeNavigation.Name;
                            _context.Set<LookUp>().Update(existingLookUpWithCode);
                        }
                    }
                }

                if (@event.Id == 0)
                {
                    // Add the event with the correct CategoryCode
                    await _context.Set<Event>().AddAsync(@event);
                }
                else
                {
                    // Find the existing event by Id
                    var existingEvent = await _context.Set<Event>().FindAsync(@event.Id);
                    if (existingEvent != null)
                    {
                        // Update the existing event's details
                        _context.Entry(existingEvent).CurrentValues.SetValues(@event);
                    }
                    else
                    {
                        throw new InvalidOperationException("Event not found.");
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;  // Re-throw the exception to propagate it
            }
        }



        public async Task DeleteEventByIdAsync(int eventId)
        {
            // Find the event by ID
            var @event = await _context.Set<Event>().FindAsync(eventId);

            if (@event != null)
            {
                // Find and delete all associated EventGuide entries
                var eventGuides = await _context.Set<EventGuide>()
                    .Where(eg => eg.EventId == eventId)
                    .ToListAsync();

                if (eventGuides.Any())
                {
                    _context.Set<EventGuide>().RemoveRange(eventGuides);
                }

                // Find and delete all associated EventMember entries
                var eventMembers = await _context.Set<EventMember>()
                    .Where(em => em.EventId == eventId)
                    .ToListAsync();

                if (eventMembers.Any())
                {
                    _context.Set<EventMember>().RemoveRange(eventMembers);
                }

                // Find the associated LookUp by matching the event's ID with the LookUp's Code
                var lookUp = await _context.Set<LookUp>().FirstOrDefaultAsync(l => l.Code == @event.CategoryCode);

                // Remove the LookUp entry if it exists
                if (lookUp != null)
                {
                    _context.Set<LookUp>().Remove(lookUp);
                }

                // Remove the event
                _context.Set<Event>().Remove(@event);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Event?> GetEventByNameAsync(string name)
        {
            return await _context.Set<Event>().SingleOrDefaultAsync(e => e.Name == name);
        }
    }
}
