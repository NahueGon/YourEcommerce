namespace YourEcommerceApi.Models.Products;

public class Brand
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? BrandImage { get; set; }

    public ICollection<Product>? Products { get; set; } = [];
}