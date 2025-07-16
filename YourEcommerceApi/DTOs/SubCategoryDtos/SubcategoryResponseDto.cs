using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.SubCategory;

public class SubcategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CategoryDto Category { get; set; } = null!;

    public List<ProductDto> Products { get; set; } = new();
}
