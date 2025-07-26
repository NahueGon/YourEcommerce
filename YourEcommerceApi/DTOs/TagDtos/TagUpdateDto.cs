namespace YourEcommerceApi.DTOs.TagDtos;

public class TagUpdateDto
{
    public required string Name { get; set; }
    public string Group { get; set; } = string.Empty;
}
