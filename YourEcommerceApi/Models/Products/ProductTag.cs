namespace YourEcommerceApi.Models.Products;

public class ProductTag
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public int TagId { get; set; }
    public Tag Tag { get; set; } = default!;
}