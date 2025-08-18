namespace YourEcommerceApi.DTOs.SportDtos;

public class SportCreateDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public IFormFile? SportImage { get; set; }
}
