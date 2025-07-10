using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.DTOs.SubCategory;

public class SubcategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public List<ProductResponseDto> Products { get; set; } = new();
}
