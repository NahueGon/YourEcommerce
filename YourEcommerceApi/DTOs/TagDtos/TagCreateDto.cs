namespace YourEcommerceApi.DTOs.TagDtos;

public class TagCreateDto
{
    public required string Name { get; set; }
    public string Group { get; set; } = string.Empty;
}