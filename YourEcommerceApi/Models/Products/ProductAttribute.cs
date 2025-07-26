namespace YourEcommerceApi.Models.Products;

public class ProductAttribute
{
    public int Id { get; set; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
