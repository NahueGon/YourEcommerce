using YourEcommerceApi.DTOs.SportDtos;

namespace YourEcommerceApi.DTOs.Product;

public class ClothResponseDto : ProductResponseDto
{
    public string? Size { get; set; }
    public SportDto? Sport { get; set; }
}
