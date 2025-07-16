using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos.AccessoryDtos;
using YourEcommerceApi.DTOs.SubCategory;

namespace YourEcommerceApi.Models;

public class Accessory : Product
{
    public required string Type { get; set; }

    public override ProductResponseDto ToDto()
    {
        return new AccessoryResponseDto
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            Stock = Stock,
            Gender = Gender,
            Type = Type,
            Brand = new BrandDto
            {
                Id = BrandId,
                Name = Brand?.Name ?? string.Empty
            },
            Subcategory = new SubcategoryDto
            {
                Id = SubcategoryId,
                Name = Subcategory?.Name ?? string.Empty
            }
        };
    }
}
