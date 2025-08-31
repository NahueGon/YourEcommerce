namespace YourEcommerceApi.DTOs.GenderDtos;

public class GenderCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; } = string.Empty;
    public IFormFile? GenderImage { get; set; }
}