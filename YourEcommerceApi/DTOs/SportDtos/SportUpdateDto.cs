namespace YourEcommerceApi.DTOs.SportDtos;

public class SportUpdateDto
{
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public IFormFile? SportImage { get; set; }
}
