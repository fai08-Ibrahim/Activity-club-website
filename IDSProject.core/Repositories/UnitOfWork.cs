using IDSProject.Repositories;
using IDSProject.core.Models;
using IDSProject.core.Repositories;
using IDSProject.core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IDSProject.Models;

namespace DemoProject.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseServerContext _context;
        private UserRepository? _userRepository;
        private MemberRepository? _memberRepository;
        private EventRepository? _eventRepository;
        private LookUpRepository? _lookUpRepository;
        private GuideRepository? _guideRepository;
        private EventMemberRepository? _eventMemberRepository;
        public UnitOfWork(DatabaseServerContext context) // Update constructor to use DatabaseServerContext
        {
            _context = context;
        }

        public IUserRepository Users =>
            _userRepository ??= new UserRepository(_context);

        UserRepository IUnitOfWork.Users => throw new NotImplementedException();

        public IMemberRepository Members => 
            _memberRepository ??= new MemberRepository(_context);

        MemberRepository IUnitOfWork.Members => throw new NotImplementedException();
        public IEventRepository Events =>
            _eventRepository ??= new EventRepository(_context);
        EventRepository IUnitOfWork.Events => throw new NotImplementedException();
        public ILookUpRepository LookUps => 
            _lookUpRepository ??= new LookUpRepository(_context);
        LookUpRepository IUnitOfWork.LookUps => throw new NotImplementedException();
        public IGuideRepository Guides =>
            _guideRepository ??= new GuideRepository(_context);
        GuideRepository IUnitOfWork.Guides => throw new NotImplementedException();
        public IEventMemberRepository EventMembers =>
            (IEventMemberRepository)(_eventMemberRepository ??= new EventMemberRepository(_context));
        EventMemberRepository IUnitOfWork.EventMembers => throw new NotImplementedException();

        // Implement other repositories as needed


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
