using IDSProject.core.Models;
using IDSProject.Models;

namespace IDSProject.core.Repositories.IRepositories
{
    public interface IEventGuideRepository
    {
        Task AddEventGuideAsync(EventGuide eventGuide);
        Task DeleteEventGuideByIdAsync(int eventGuideId);
        Task<IEnumerable<EventGuide>> GetAllByGuideIdAsync(int guideId);
        Task<IEnumerable<EventGuide>> GetAllByEventIdAsync(int eventId);
        Task<EventGuide?> DeleteByGuideIdAndEventNameAsync(int guideId, string eventName);
        Task<IEnumerable<EventGuide>> DeleteAllByEventNameAsync(string eventName);
    }
}
