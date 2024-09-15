using IDSProject.Models;
namespace IDSProject.core.Repositories.IRepositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member?> GetByIdAsync(int id);
        Task AddOrUpdateMemberAsync(Member member);
        Task DeleteMemberByIdAsync(int memberId);
        Task<Member?> GetMemberByEmailAsync(string email);

    }
}