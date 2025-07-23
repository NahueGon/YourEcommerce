namespace YourEcommerce.ViewModels;

public class ProductTypeViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public List<SubcategoryViewModel> Subcategories { get; set; } = new();
}
