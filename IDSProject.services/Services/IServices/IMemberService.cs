using IDSProject.Models;

namespace IDSProject.services.Services

{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member?> GetByMemberIdAsync(int id);
        Task AddOrUpdateMember(Member member);
        Task DeleteMemberById(int memberId);
        Task<Member?> GetMemberByEmail(string email);
    }
}