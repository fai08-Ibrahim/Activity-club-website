using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.core.Dtos;
using IDSProject.Models;

namespace IDSProject.services.Services
{
    public interface IEventMemberService
    {
        Task AddEventMemberAsync(EventMember eventMember);
        Task DeleteEventMemberByIdAsync(int eventMemberId);
        Task<IEnumerable<EventMember>> GetAllByMemberIdAsync(int memberId);
        Task<IEnumerable<EventMember>> GetAllByEventIdAsync(int eventId);
        Task<EventMember?> DeleteByMemberIdAndEventNameAsync(int memberId, string eventName);
        Task<IEnumerable<EventMember>> DeleteAllByEventNameAsync(string eventName);
    }
}