namespace YourEcommerceApi.Models;

public class Cloth : Product
{
    public string? Size { get; set; }

    public int SportId { get; set; }
    public Sport? Sport { get; set; }
}
