using AutoMapper;
using InvitationManagerAPI.Dtos.User;
using InvitationManagerAPI.Models;

namespace InvitationManagerAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, GetPersonDto>();

            CreateMap<AddPersonDto, Person>();
        }
    }
}
