namespace YourEcommerceApi.DTOs.ProductType;

public class ProductTypeCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int CategoryId { get; set; }
}
