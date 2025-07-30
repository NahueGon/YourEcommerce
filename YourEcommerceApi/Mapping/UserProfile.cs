using AutoMapper;
using YourEcommerceApi.DTOs.AuthDtos.LoginDtos;
using YourEcommerceApi.DTOs.AuthDtos.RegisterDtos;
using YourEcommerceApi.DTOs.UserDtos;
using YourEcommerceApi.Models.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegisterCreateDto, User>();
        CreateMap<User, UserRegisterResponseDto>();

        CreateMap<User, UserLoginResponseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Name} {src.Lastname}"));
            
        CreateMap<User, UserResponseDto>();
        CreateMap<User, UserDto>();
        CreateMap<UserCreateDto, User>();
    }
}