using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.TagDtos;

namespace YourEcommerceApi.DTOs.ProductTagDtos;

public class ProductTagResponseDto
{
    public required ProductDto Product { get; set; }
    public required TagDto Tag { get; set; }
}