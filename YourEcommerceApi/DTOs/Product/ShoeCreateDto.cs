namespace YourEcommerceApi.DTOs.Product;

public class ShoeCreateDto : ProductCreateDto
{
    public int SportId { get; set; }
    public int Size { get; set; }
    public string? Model { get; set; }
}