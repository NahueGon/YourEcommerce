using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.DTOs.ProductImageDtos;

namespace YourEcommerceApi.DTOs.ProductVariantDtos;

public class ProductVariantCreateDto
{
    public string Size { get; set; } = string.Empty;
    public int Stock { get; set; }

    public ICollection<ProductColorCreateDto> Colors { get; set; } = new List<ProductColorCreateDto>();
    public ICollection<ProductImageCreateDto> Images { get; set; } = new List<ProductImageCreateDto>();
}