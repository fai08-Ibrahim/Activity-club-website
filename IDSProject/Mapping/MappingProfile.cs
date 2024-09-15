using AutoMapper;
using IDSProject.core.Models;
using IDSProject.core.Dtos;
using IDSProject.Models;

namespace IDSProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user mapping
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            //event mapping
            CreateMap<Event, EventDto>().ReverseMap();

            //member mapping
            CreateMap<MemberDto, Member>();
            CreateMap<Member, MemberDto>();

            //guide mapping
            CreateMap<GuideDto, Guide>().ReverseMap();

            //eventMember mapping
            CreateMap<EventMember, EventMemberDto>().ReverseMap();

            //eventGuide mapping
            CreateMap<EventGuideDto, EventGuide>().ReverseMap();
        }
    }
}