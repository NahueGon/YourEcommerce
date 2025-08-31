using AutoMapper;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.CategoryGender;
using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.DTOs.GenderDtos;

public class CategoryGendersProfile : Profile
{
    public CategoryGendersProfile()
    {
        CreateMap<CategoryGender, CategoryGendersResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryGenderImage, opt => opt.MapFrom(src => src.CategoryGenderImage))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

        CreateMap<Category, CategoryDto>();

        CreateMap<CategoryGender, CategoryGendersDto>();

        CreateMap<Gender, GenderDto>();

        CreateMap<CategoryGenderCreateDto, CategoryGender>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}