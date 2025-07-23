namespace YourEcommerceApi.DTOs.ProductType;

public class ProductTypeUpdateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; } 
    public int CategoryId { get; set; }
}
