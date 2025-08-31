namespace YourEcommerceApi.Models.Products;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? CategoryImage { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<CategoryGender> CategoryGenders { get; set; } = new List<CategoryGender>();
}