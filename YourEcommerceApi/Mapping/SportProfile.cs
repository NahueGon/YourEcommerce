using AutoMapper;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.DTOs.ProductDtos;

public class SportProfile : Profile
{
    public SportProfile()
    {
        CreateMap<Sport, SportDto>();
        CreateMap<SportCreateDto, Sport>();
        CreateMap<Sport, SportResponseDto>();
        
        CreateMap<Product, ProductDto>();
    }
}