using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.Sport;

public class SportResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public List<ProductDto> Products { get; set; } = new();
}
