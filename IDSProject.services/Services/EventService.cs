using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;
using IDSProject.core.Repositories.IRepositories;
using IDSProject.core.Services.IServices;

namespace IDSProject.core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllEvents();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task AddOrUpdateEventAsync(Event @event)
        {
            await _eventRepository.AddOrUpdateEventAsync(@event);
        }

        public async Task DeleteEventByIdAsync(int id)
        {
            await _eventRepository.DeleteEventByIdAsync(id);
        }

        public async Task<Event?> GetEventByNameAsync(string name)
        {
            return await _eventRepository.GetEventByNameAsync(name);
        }
    }
}
