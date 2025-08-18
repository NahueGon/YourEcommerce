using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.SportDtos;

public class SportResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? SportImage { get; set; }
    
    public List<ProductDto> Products { get; set; } = [];
}
