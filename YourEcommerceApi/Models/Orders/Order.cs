using YourEcommerceApi.Models.Carts;
using YourEcommerceApi.Models.Users;

namespace YourEcommerceApi.Models.Orders;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public List<CartItem> Items { get; set; } = [];
}
