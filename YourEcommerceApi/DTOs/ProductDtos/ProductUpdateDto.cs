using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.DTOs.Product;

public class ProductUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Gender Gender { get; set; }

    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public int? SportId { get; set; }
}