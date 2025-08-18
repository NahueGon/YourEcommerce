using AutoMapper;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.GenderDtos;
using YourEcommerceApi.DTOs.ProductDtos;

public class GenderProfile : Profile
{
    public GenderProfile()
    {
        CreateMap<GenderCreateDto, Gender>();
        CreateMap<Gender, GenderDto>();
        CreateMap<Gender, GenderResponseDto>();
        
        CreateMap<Product, ProductDto>();
    }
}