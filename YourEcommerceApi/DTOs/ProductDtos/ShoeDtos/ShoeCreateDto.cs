namespace YourEcommerceApi.DTOs.Product;

public class ShoeCreateDto : ProductCreateDto
{
    public string? Size { get; set; }
    public string? Model { get; set; }
    public int SportId { get; set; }
}