using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;

namespace IDSProject.core.Services.IServices
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task AddOrUpdateEventAsync(Event @event);
        Task DeleteEventByIdAsync(int id);
        Task<Event?> GetEventByNameAsync(string name);
    }
}
