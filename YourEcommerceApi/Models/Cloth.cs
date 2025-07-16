using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.DTOs.SubCategory;

namespace YourEcommerceApi.Models;

public class Cloth : Product
{
    public string? Size { get; set; }
    public int SportId { get; set; }
    public Sport? Sport { get; set; }

    public override ProductResponseDto ToDto()
    {
        return new ClothResponseDto
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            Stock = Stock,
            Gender = Gender,
            Size = Size,
            Sport = new SportDto
            {
                Id = SportId,
                Name = Sport?.Name ?? string.Empty
            },
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
