using YourEcommerceApi.DTOs.ProductVariantDtos;

namespace YourEcommerceApi.DTOs.ProductColorDtos;

public class ProductColorResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<ProductVariantDto> ProductVariants { get; set; } = [];
}