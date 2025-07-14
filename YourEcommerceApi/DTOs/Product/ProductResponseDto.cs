using YourEcommerceApi.Models;

namespace YourEcommerceApi.DTOs.Product;

public class ProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Gender Gender { get; set; }
    public int BrandId { get; set; }
    public int SubcategoryId { get; set; }

}
