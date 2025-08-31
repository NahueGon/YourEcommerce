namespace YourEcommerceApi.Models.Products;

public class ProductVariant
{
    public int Id { get; set; }
    public string Size { get; set; } = string.Empty;
    public int Stock { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<ProductColor> Colors { get; set; } = new List<ProductColor>();
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}