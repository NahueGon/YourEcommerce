using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.Category;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public List<ProductDto> Products { get; set; } = [];
}