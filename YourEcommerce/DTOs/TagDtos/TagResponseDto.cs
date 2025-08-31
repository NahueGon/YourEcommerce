namespace YourEcommerce.DTOs.TagDtos;

public class TagResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;

    public override string ToString() => Name;
}