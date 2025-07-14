using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.DTOs.Sport;

public class SportResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public List<ProductResponseDto> Products { get; set; } = new();
}
