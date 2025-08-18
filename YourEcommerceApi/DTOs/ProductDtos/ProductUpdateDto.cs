using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;

namespace YourEcommerceApi.DTOs.Product;

public class ProductUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public int? GenderId { get; set; }
    public int? SportId { get; set; }

    public ICollection<ProductVariantDto>? ProductVariants { get; set; }
    public ICollection<ProductAttributeDto>? ProductAttributes { get; set; }
    public ICollection<ProductTagDto>? ProductTags { get; set; }
}