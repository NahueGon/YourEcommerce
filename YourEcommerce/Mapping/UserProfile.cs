using AutoMapper;
using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserResponseDto>()
            .ForMember(dest => dest.ProfileImage,
                       opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ProfileImage)
                           ? "/img/anonymous-profile.png"
                           : src.ProfileImage))
            .ReverseMap();

            CreateMap<UserResponseDto, UserUpdateDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImage ?? "/img/anonymous-profile.png"));

            CreateMap<UserUpdateDto, UserResponseDto>();

            CreateMap<UserDto, UserUpdateDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());
                
            CreateMap<UserUpdateDto, UserDto>();
        }
    }
}