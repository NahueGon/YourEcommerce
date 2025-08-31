using AutoMapper;
using YourEcommerce.DTOs.CategoryDtos;

namespace YourEcommerce.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, CategoryResponseDto>()
            .ForMember(dest => dest.CategoryImage,
                       opt => opt.MapFrom(src => string.IsNullOrEmpty(src.CategoryImage)
                           ? "/img/anonymous-profile.png"
                           : src.CategoryImage))
            .ReverseMap();

            CreateMap<CategoryResponseDto, CategoryUpdateDto>()
                .ForMember(dest => dest.CategoryImage, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryImageUrl, opt => opt.MapFrom(src => src.CategoryImage ?? "/img/anonymous-profile.png"));

            CreateMap<CategoryUpdateDto, CategoryResponseDto>();

            CreateMap<CategoryDto, CategoryUpdateDto>()
                .ForMember(dest => dest.CategoryImage, opt => opt.Ignore());
                
            CreateMap<CategoryUpdateDto, CategoryDto>();
        }
    }
}