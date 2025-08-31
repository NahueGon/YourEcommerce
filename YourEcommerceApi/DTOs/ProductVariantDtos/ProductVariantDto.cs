using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.DTOs.ProductImageDtos;

namespace YourEcommerceApi.DTOs.ProductVariantDtos;

public class ProductVariantDto
{
    public string Size { get; set; } = string.Empty;
    public int Stock { get; set; }

    public ICollection<ProductColorDto> Colors { get; set; } = new List<ProductColorDto>();
    public ICollection<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();
}