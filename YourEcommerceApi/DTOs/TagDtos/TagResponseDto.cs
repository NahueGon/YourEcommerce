using YourEcommerceApi.DTOs.ProductTagDtos;

namespace YourEcommerceApi.DTOs.TagDtos;

public class TagResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Group { get; set; } = string.Empty;
    
    public ICollection<ProductTagDto> ProductTags { get; set; } = [];
}
