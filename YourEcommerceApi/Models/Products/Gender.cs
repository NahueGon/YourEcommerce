namespace YourEcommerceApi.Models.Products;

public class Gender
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? GenderImage { get; set; } = string.Empty;

    public ICollection<Product>? Products { get; set; } = new List<Product>();
}