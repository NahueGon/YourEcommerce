using AutoMapper;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.ProductDtos;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandCreateDto, Brand>();
        CreateMap<Brand, BrandDto>();
        CreateMap<Brand, BrandResponseDto>();
        
        CreateMap<Product, ProductDto>();
    }
}