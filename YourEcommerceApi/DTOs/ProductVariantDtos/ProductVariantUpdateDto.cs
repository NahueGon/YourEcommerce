using YourEcommerceApi.DTOs.ProductColorDtos;

namespace YourEcommerceApi.DTOs.ProductVariantDtos;

public class ProductVariantUpdateDto
{
    public int ProductId { get; set; }
    public string Size { get; set; } = string.Empty;
    public int Stock { get; set; }

    public ICollection<ProductColorDto> Colors { get; set; } = new List<ProductColorDto>();
}