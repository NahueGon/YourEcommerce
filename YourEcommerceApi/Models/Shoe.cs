namespace YourEcommerceApi.Models;

public class Shoe : Product
{
    public int? Size { get; set; }
    public string? Model { get; set; }

    public int SportId { get; set; }
    public Sport? Sport { get; set; }
}
