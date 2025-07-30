namespace YourEcommerceApi.DTOs.TagDtos;

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    private string? _group;
    public string? Group
    {
        get => string.IsNullOrWhiteSpace(_group) ? null : _group;
        set => _group = value;
    }
}
