using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.BrandDtos;

public class BrandResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? BrandImage { get; set; }

    public List<ProductDto> Products { get; set; } = [];
}