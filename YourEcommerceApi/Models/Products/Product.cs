namespace YourEcommerceApi.Models.Products;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Gender Gender { get; set; }

    public int? BrandId { get; set; }
    public Brand? Brand { get; set; }

    public int? SportId { get; set; }
    public Sport? Sport { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
    public ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}