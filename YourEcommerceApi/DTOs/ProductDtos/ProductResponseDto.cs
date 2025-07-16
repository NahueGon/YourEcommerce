using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.SubCategory;
using YourEcommerceApi.Models;
using System.Text.Json.Serialization;
using YourEcommerceApi.DTOs.ProductDtos.ShoeDtos;
using YourEcommerceApi.DTOs.ProductDtos.AccessoryDtos;

namespace YourEcommerceApi.DTOs.Product;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "productType")]
[JsonDerivedType(typeof(ClothResponseDto), "Cloth")]
[JsonDerivedType(typeof(ShoeResponseDto), "Shoe")]
[JsonDerivedType(typeof(AccessoryResponseDto), "Accessory")]
public class ProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Gender Gender { get; set; }
    public BrandDto? Brand { get; set; }
    public SubcategoryDto? Subcategory { get; set; }
}
