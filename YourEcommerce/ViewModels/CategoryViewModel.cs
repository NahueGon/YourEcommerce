namespace YourEcommerce.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ProductViewModel> Products { get; set; } = new();
    public int ProductCount => Products?.Count ?? 0;
}