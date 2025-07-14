using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.DTOs.SubCategory;

public class SubcategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CategoryDto Category { get; set; } = null!;

    public List<ProductResponseDto> Products { get; set; } = new();
}
