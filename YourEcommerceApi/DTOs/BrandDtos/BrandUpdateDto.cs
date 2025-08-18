namespace YourEcommerceApi.DTOs.BrandDtos;

public class BrandUpdateDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public IFormFile? BrandImage { get; set; }
}