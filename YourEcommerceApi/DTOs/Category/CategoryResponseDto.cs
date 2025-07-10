using YourEcommerceApi.DTOs.SubCategory;

namespace YourEcommerceApi.DTOs.Category;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public List<SubcategoryResponseDto> Subcategories { get; set; } = new();
}
