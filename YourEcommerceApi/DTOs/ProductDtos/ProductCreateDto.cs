using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.DTOs.ProductDtos;

public class ProductCreateDto
{
    public required string Name { get; set; }
    public string Description { get; set; }  = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Gender Gender { get; set; }
    public int? BrandId { get; set; }
    public int? SportId { get; set; }
    public int? CategoryId { get; set; }
}