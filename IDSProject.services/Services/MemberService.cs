using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;
using IDSProject.core.Repositories;
using IDSProject.core.Repositories.IRepositories;

namespace IDSProject.services.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = (IMemberRepository?)memberRepository;
        }

        public async Task<Member?> GetByMemberIdAsync(int memberId) // Implement the method
        {
            return await _memberRepository.GetByIdAsync(memberId);
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _memberRepository.GetAllMembers();
        }
        public async Task DeleteMemberById(int memberId)
        {
            await _memberRepository.DeleteMemberByIdAsync(memberId);
        }
        public async Task AddOrUpdateMember(Member member)
        {
            await _memberRepository.AddOrUpdateMemberAsync(member);
        }
        public async Task<Member?> GetMemberByEmail(string email)
        {
            return await _memberRepository.GetMemberByEmailAsync(email);
        }
    }
}

