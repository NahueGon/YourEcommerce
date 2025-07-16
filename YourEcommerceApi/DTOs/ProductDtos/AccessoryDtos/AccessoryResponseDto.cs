using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.DTOs.ProductDtos.AccessoryDtos;

public class AccessoryResponseDto : ProductResponseDto
{
    public required string Type { get; set; }
}
