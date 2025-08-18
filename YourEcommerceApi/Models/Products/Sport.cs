namespace YourEcommerceApi.Models.Products;

public class Sport
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? SportImage { get; set; } = string.Empty;

    public ICollection<Product>? Products { get; set; } = new List<Product>();
}