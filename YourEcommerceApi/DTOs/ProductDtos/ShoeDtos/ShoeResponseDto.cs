using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.SportDtos;

namespace YourEcommerceApi.DTOs.ProductDtos.ShoeDtos;

public class ShoeResponseDto : ProductResponseDto
{
    public string? Size { get; set; }
    public string? Model { get; set; }
    public SportDto? Sport { get; set; }
}
