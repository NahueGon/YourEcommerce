namespace YourEcommerceApi.Models.Products;

public class ProductImage
{
    public int Id { get; set; }
    public required string ImageUrl { get; set; }

    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!;
}