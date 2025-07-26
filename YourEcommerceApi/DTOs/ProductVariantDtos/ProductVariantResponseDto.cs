using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.ProductVariantDtos;

public class ProductVariantResponseDto
{
    public int Id { get; set; }
    public string Size { get; set; } = string.Empty;
    public int Stock { get; set; }

    public int? ProductId { get; set; }
    public ProductDto? Product { get; set; }

    public ICollection<ProductColorDto> Colors { get; set; } = new List<ProductColorDto>();
}