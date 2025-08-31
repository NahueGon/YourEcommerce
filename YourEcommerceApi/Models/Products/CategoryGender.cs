namespace YourEcommerceApi.Models.Products;

public class CategoryGender
{
    public int Id { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public int GenderId { get; set; }
    public Gender? Gender { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CategoryGenderImage { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}