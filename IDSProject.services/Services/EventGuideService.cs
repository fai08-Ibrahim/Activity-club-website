using IDSProject.core.Dtos;
using IDSProject.core.Models;
using IDSProject.core.Repositories.IRepositories;
using IDSProject.Models;

namespace IDSProject.core.Services
{
    public class EventGuideService : IEventGuideService
    {
        private readonly IEventGuideRepository _eventGuideRepo;

        public EventGuideService(IEventGuideRepository eventGuideRepo)
        {
            _eventGuideRepo = eventGuideRepo;
        }

        public async Task AddEventGuideAsync(EventGuide eventGuide)
        {
            await _eventGuideRepo.AddEventGuideAsync(eventGuide);
        }

        public async Task DeleteEventGuideByIdAsync(int eventGuideId)
        {
            await _eventGuideRepo.DeleteEventGuideByIdAsync(eventGuideId);
        }

        public async Task<IEnumerable<EventGuide>> GetAllByGuideIdAsync(int guideId)
        {
            return await _eventGuideRepo.GetAllByGuideIdAsync(guideId);
        }

        public async Task<IEnumerable<EventGuide>> GetAllByEventIdAsync(int eventId)
        {
            return await _eventGuideRepo.GetAllByEventIdAsync(eventId);
        }

        public async Task<EventGuide?> DeleteByGuideIdAndEventNameAsync(int guideId, string eventName)
        {
            return await _eventGuideRepo.DeleteByGuideIdAndEventNameAsync(guideId, eventName);
        }

        public async Task<IEnumerable<EventGuide>> DeleteAllByEventNameAsync(string eventName)
        {
            return await _eventGuideRepo.DeleteAllByEventNameAsync(eventName);
        }
    }
}
