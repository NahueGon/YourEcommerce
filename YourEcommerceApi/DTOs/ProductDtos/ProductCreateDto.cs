using YourEcommerceApi.Models;

namespace YourEcommerceApi.DTOs.Product;

public abstract class ProductCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Gender Gender { get; set; }
    public int BrandId { get; set; }
    public int SubcategoryId { get; set; }
}