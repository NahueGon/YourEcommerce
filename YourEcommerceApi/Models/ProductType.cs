namespace YourEcommerceApi.Models;
public class ProductType
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<SubCategory> Subcategories { get; set; } = new List<SubCategory>();
}