using YourEcommerceApi.DTOs.SubCategory;

namespace YourEcommerceApi.DTOs.ProductType;

public class ProductTypeResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<SubcategoryResponseDto> SubCategories { get; set; } = new();
}
