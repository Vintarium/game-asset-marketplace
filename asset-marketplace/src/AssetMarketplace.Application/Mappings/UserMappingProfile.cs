using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Domain.Entities;
using AutoMapper;

namespace AssetMarketplace.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }
}
