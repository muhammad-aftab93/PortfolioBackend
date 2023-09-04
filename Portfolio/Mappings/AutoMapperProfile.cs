using AutoMapper;

namespace Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Database.Entities.User, Models.User>()
                .ReverseMap();

            CreateMap<Models.CreateUserRequest, Database.Entities.User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Database.Entities.PersonalDetails, Models.PersonalDetails>()
                .ReverseMap();

            CreateMap<Models.CreatePersonalDetailsRequest, Database.Entities.PersonalDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
