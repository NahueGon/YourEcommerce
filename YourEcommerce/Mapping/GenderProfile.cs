using AutoMapper;
using YourEcommerce.DTOs.GenderDtos;

namespace YourEcommerce.Mapping
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<GenderDto, GenderResponseDto>()
            .ForMember(dest => dest.GenderImage,
                       opt => opt.MapFrom(src => string.IsNullOrEmpty(src.GenderImage)
                           ? "/img/anonymous-profile.png"
                           : src.GenderImage))
            .ReverseMap();

            CreateMap<GenderResponseDto, GenderUpdateDto>()
                .ForMember(dest => dest.GenderImage, opt => opt.Ignore())
                .ForMember(dest => dest.GenderImageUrl, opt => opt.MapFrom(src => src.GenderImage ?? "/img/anonymous-profile.png"));

            CreateMap<GenderUpdateDto, GenderResponseDto>();

            CreateMap<GenderDto, GenderUpdateDto>()
                .ForMember(dest => dest.GenderImage, opt => opt.Ignore());
                
            CreateMap<GenderUpdateDto, GenderDto>();
        }
    }
}