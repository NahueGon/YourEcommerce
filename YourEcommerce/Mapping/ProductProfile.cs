using AutoMapper;
using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, ProductResponseDto>()
                .ReverseMap();

            CreateMap<ProductResponseDto, ProductUpdateDto>()
                .ReverseMap();

            CreateMap<ProductUpdateDto, ProductResponseDto>();

            CreateMap<ProductDto, ProductUpdateDto>()
                .ReverseMap();

            CreateMap<ProductViewModel, ProductResponseDto>();
            CreateMap<ProductResponseDto, ProductViewModel>();
            CreateMap<ProductViewModel, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, ProductViewModel>();
        }
    }
}
