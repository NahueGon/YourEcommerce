namespace YourEcommerceApi.Models;

public class Sport
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public ICollection<Shoe>? Shoes { get; set; } = new List<Shoe>();
    public ICollection<Cloth>? Clothes { get; set; } = new List<Cloth>();
}
