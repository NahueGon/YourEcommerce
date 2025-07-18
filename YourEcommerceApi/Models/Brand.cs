namespace YourEcommerceApi.Models;

public class Brand
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Product>? Products { get; set; } = new List<Product>();
}
