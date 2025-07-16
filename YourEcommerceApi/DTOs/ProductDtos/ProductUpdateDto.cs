namespace YourEcommerceApi.DTOs.Product;

public class ProductUpdateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; } 
    public int SubcategoryId { get; set; }
}
