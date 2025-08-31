using AutoMapper;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
            // Mapeos para Productos
            CreateMap<ProductViewModel, ProductResponseDto>();
            CreateMap<ProductResponseDto, ProductViewModel>();
            CreateMap<ProductViewModel, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, ProductViewModel>();

            // Mapeos para Categorías
            CreateMap<CategoryViewModel, CategoryResponseDto>();
            CreateMap<CategoryResponseDto, CategoryViewModel>();
    }
}