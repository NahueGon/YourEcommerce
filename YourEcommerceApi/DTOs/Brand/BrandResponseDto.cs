using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.DTOs.Brand;

public class BrandResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public List<ProductResponseDto> Products { get; set; } = new();
}
