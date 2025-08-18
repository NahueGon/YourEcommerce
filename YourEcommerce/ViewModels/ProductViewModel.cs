namespace YourEcommerce.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TotalStock { get; set; }
    public bool IsActive { get; set; } = true;

    public CategoryViewModel Category { get; set; } = new();
    public SportViewModel Sport { get; set; } = new();
    public BrandViewModel Brand { get; set; } = new();
    public GenderViewModel Gender { get; set; } = new();
    public ICollection<ProductTagViewModel> ProductTags { get; set; } = new List<ProductTagViewModel>();

    public string CategoryName => Category?.Name ?? "-";
    public string SportName => Sport?.Name ?? "-";
    public string BrandName => Brand?.Name ?? "-";
    public string GenderName => Gender?.Name ?? "-";
    public string ProductTagsNames => string.Join(", ", ProductTags.Select(t => t.Tag.Name));
}