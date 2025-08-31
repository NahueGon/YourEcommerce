using AutoMapper;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.GenderDtos;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.GenderProductsDtos;
using YourEcommerceApi.DTOs.CategoryGender;

public class GenderProfile : Profile
{
    public GenderProfile()
    {
        CreateMap<GenderCreateDto, Gender>();
        CreateMap<Gender, GenderDto>();
        CreateMap<Gender, GenderResponseDto>();
        CreateMap<Gender, GenderProductsDto>();
        CreateMap<Gender, CategoryGendersDto>();
        CreateMap<Product, ProductDto>();
    }
}