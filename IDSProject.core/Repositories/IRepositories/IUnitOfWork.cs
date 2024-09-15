using IDSProject.Models;

namespace IDSProject.core.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; }
        MemberRepository Members { get; }
        EventRepository Events { get; }
        LookUpRepository LookUps { get; }
        GuideRepository Guides { get; }
        EventMemberRepository EventMembers { get; }
    }
}