using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;

namespace IDSProject.core.Repositories.IRepositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event?> GetByIdAsync(int id);
        Task AddOrUpdateEventAsync(Event @event);
        Task DeleteEventByIdAsync(int eventId);
        Task<Event?> GetEventByNameAsync(string name);
    }
}
