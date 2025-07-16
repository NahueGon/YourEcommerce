namespace YourEcommerceApi.DTOs.Product;

public class ClothCreateDto : ProductCreateDto
{
    public string Size { get; set; } = default!;
    public int SportId { get; set; }
}
