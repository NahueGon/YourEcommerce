using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Models.Carts;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    
    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;
    
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
