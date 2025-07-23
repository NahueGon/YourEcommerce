namespace YourEcommerceApi.Models;

public class SubCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }

    public ICollection<Product>? Products { get; set; }
}
