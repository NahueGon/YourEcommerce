namespace YourEcommerceApi.DTOs.BrandDtos;

public class BrandCreateDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
}
