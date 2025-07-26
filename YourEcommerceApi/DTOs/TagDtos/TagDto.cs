namespace YourEcommerceApi.DTOs.TagDtos;

public class TagDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Group { get; set; } = string.Empty;
}
