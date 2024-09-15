using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;
using IDSProject.core.Repositories.IRepositories;

namespace IDSProject.services.Services
{
    public class EventMemberService : IEventMemberService
    {
        private readonly IEventMemberRepository _eventMemberRepository;

        public EventMemberService(IEventMemberRepository eventMemberRepository)
        {
            _eventMemberRepository = eventMemberRepository;
        }

        public async Task AddEventMemberAsync(EventMember eventMember)
        {
            await _eventMemberRepository.AddEventMemberAsync(eventMember);
        }

        public async Task DeleteEventMemberByIdAsync(int eventMemberId)
        {
            await _eventMemberRepository.DeleteEventMemberByIdAsync(eventMemberId);
        }

        public async Task<IEnumerable<EventMember>> GetAllByMemberIdAsync(int memberId)
        {
            return await _eventMemberRepository.GetAllByMemberIdAsync(memberId);
        }

        public async Task<IEnumerable<EventMember>> GetAllByEventIdAsync(int eventId)
        {
            return await _eventMemberRepository.GetAllByEventIdAsync(eventId);
        }
        public async Task<EventMember?> DeleteByMemberIdAndEventNameAsync(int memberId, string eventName)
        {
            return await _eventMemberRepository.DeleteByMemberIdAndEventNameAsync(memberId, eventName);
        }

        public async Task<IEnumerable<EventMember>> DeleteAllByEventNameAsync(string eventName)
        {
            return await _eventMemberRepository.DeleteAllByEventNameAsync(eventName);
        }
    }
}
