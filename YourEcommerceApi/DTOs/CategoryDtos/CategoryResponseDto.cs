using YourEcommerceApi.DTOs.ProductType;

namespace YourEcommerceApi.DTOs.Category;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public List<ProductTypeResponseDto> ProductTypes { get; set; } = new();
}
