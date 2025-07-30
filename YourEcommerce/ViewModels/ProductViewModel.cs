namespace YourEcommerce.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TotalStock { get; set; }
    public bool IsActive { get; set; } = true;
    public Gender Gender { get; set; } = Gender.Unisex;

    public CategoryViewModel Category { get; set; } = new();
    public ICollection<ProductTagViewModel> ProductTags { get; set; } = new List<ProductTagViewModel>();
}
