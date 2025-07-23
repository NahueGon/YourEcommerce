using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductType;

namespace YourEcommerceApi.DTOs.SubCategory;

public class SubcategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<ProductResponseDto> Products { get; set; } = new();
}
