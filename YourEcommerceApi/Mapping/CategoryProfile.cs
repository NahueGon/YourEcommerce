using AutoMapper;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.CategoryDtos;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<Category, CategoryDto>();

        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();
    }
}