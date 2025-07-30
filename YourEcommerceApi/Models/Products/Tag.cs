namespace YourEcommerceApi.Models.Products;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Group { get; set; } 
    
    public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}