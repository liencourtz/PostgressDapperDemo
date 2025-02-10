using AutoMapper;
using PostgressDapperDemo.API.Models.Domain;
using PostgressDapperDemo.API.Models.DTOs;

namespace PostgressDapperDemo.API.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDisplayDto>().ReverseMap();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
        }
    }
}
