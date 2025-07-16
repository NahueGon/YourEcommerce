using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.Brand;

public class BrandResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public List<ProductDto> Products { get; set; } = new();
}
