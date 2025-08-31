using YourEcommerce.DTOs.TagDtos;

namespace YourEcommerce.DTOs.ProductTagDtos;

public class ProductTagResponseDto
{
    public TagResponseDto Tag { get; set; } = new TagResponseDto();

    public override string ToString() => Tag?.Name ?? string.Empty;
}