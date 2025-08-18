namespace YourEcommerceApi.DTOs.GenderDtos;

public class GenderUpdateDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public IFormFile? GenderImage { get; set; }
}