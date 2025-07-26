using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class BrandExtension
{
    public static BrandResponseDto ToDto(this Brand brand)
    {
        return new BrandResponseDto
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }
}