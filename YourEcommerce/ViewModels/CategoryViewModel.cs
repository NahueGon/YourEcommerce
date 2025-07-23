namespace YourEcommerce.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public List<ProductTypeViewModel> ProductTypes { get; set; } = new();

}
