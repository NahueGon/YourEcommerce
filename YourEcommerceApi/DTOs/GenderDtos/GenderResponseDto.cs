using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.GenderDtos;

public class GenderResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? GenderImage { get; set; }
    
    public List<ProductDto> Products { get; set; } = [];
}