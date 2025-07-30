using AutoMapper;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.CategoryDtos;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryResponseDto>();
    }
}