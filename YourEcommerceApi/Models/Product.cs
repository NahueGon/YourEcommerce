using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.Models;

public abstract class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Gender Gender { get; set; }

    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    public int SubcategoryId { get; set; }
    public SubCategory? Subcategory { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public abstract ProductResponseDto ToDto();
}
