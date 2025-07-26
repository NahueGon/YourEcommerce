namespace YourEcommerceApi.DTOs.SportDtos;

public class SportUpdateDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
}
