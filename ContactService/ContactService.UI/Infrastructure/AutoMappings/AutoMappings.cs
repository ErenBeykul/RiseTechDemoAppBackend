using AutoMapper;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;

namespace ContactService.UI.AutoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonData, Person>().ReverseMap();
        }
    }
}