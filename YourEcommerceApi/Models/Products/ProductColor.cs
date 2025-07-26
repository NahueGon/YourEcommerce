namespace YourEcommerceApi.Models.Products;

public class ProductColor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!;
}
