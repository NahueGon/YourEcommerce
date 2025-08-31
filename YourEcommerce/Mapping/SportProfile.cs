using AutoMapper;
using YourEcommerce.DTOs.SportDtos;

namespace YourEcommerce.Mapping
{
    public class SportProfile : Profile
    {
        public SportProfile()
        {
            CreateMap<SportDto, SportResponseDto>()
            .ForMember(dest => dest.SportImage,
                       opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SportImage)
                           ? "/img/anonymous-profile.png"
                           : src.SportImage))
            .ReverseMap();

            CreateMap<SportResponseDto, SportUpdateDto>()
                .ForMember(dest => dest.SportImage, opt => opt.Ignore())
                .ForMember(dest => dest.SportImageUrl, opt => opt.MapFrom(src => src.SportImage ?? "/img/anonymous-profile.png"));

            CreateMap<SportUpdateDto, SportResponseDto>();

            CreateMap<SportDto, SportUpdateDto>()
                .ForMember(dest => dest.SportImage, opt => opt.Ignore());
                
            CreateMap<SportUpdateDto, SportDto>();
        }
    }
}