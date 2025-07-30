using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.DTOs.ProductDtos;

public class ProductCreateDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Gender Gender { get; set; }
    
    public int? BrandId { get; set; }
    public int? SportId { get; set; }
    public int? CategoryId { get; set; }

    public ICollection<ProductVariantCreateDto> ProductVariants { get; set; } = new List<ProductVariantCreateDto>();
    public ICollection<ProductAttributeCreateDto> ProductAttributes { get; set; } = new List<ProductAttributeCreateDto>();
    public ICollection<ProductTagCreateDto> ProductTags { get; set; } = new List<ProductTagCreateDto>();
}