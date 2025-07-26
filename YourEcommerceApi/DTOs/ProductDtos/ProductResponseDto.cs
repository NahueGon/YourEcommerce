using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.ProductVariantDtos;

namespace YourEcommerceApi.DTOs.ProductDtos;

public class ProductResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    
    public Gender Gender { get; set; } = Gender.Unknown;
    public BrandDto? Brand { get; set; }
    public SportDto? Sport { get; set; }
    public CategoryDto? Category { get; set; }

    public ICollection<ProductTagResponseDto> ProductTags { get; set; } = new List<ProductTagResponseDto>();
    public ICollection<ProductAttributeDto> ProductAttributes { get; set; } = new List<ProductAttributeDto>();
    public ICollection<ProductVariantDto> ProductVariants { get; set; } = new List<ProductVariantDto>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
