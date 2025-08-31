using AutoMapper;
using YourEcommerce.DTOs.BrandDtos;

namespace YourEcommerce.Mapping
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandDto, BrandResponseDto>()
            .ForMember(dest => dest.BrandImage,
                       opt => opt.MapFrom(src => string.IsNullOrEmpty(src.BrandImage)
                           ? "/img/anonymous-profile.png"
                           : src.BrandImage))
            .ReverseMap();

            CreateMap<BrandResponseDto, BrandUpdateDto>()
                .ForMember(dest => dest.BrandImage, opt => opt.Ignore())
                .ForMember(dest => dest.BrandImageUrl, opt => opt.MapFrom(src => src.BrandImage ?? "/img/anonymous-profile.png"));

            CreateMap<BrandUpdateDto, BrandResponseDto>();

            CreateMap<BrandDto, BrandUpdateDto>()
                .ForMember(dest => dest.BrandImage, opt => opt.Ignore());
                
            CreateMap<BrandUpdateDto, BrandDto>();
        }
    }
}