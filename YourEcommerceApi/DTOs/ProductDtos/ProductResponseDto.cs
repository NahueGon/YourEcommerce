using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.DTOs.GenderDtos;

namespace YourEcommerceApi.DTOs.ProductDtos;

public class ProductResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TotalStock { get; set; }
    public bool IsActive { get; set; } = true;
    
    public GenderDto? Gender { get; set; }
    public BrandDto? Brand { get; set; }
    public SportDto? Sport { get; set; }
    public CategoryDto? Category { get; set; }

    public ICollection<ProductTagDto> ProductTags { get; set; } = new List<ProductTagDto>();
    public ICollection<ProductAttributeDto> ProductAttributes { get; set; } = new List<ProductAttributeDto>();
    public ICollection<ProductVariantDto> ProductVariants { get; set; } = new List<ProductVariantDto>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}