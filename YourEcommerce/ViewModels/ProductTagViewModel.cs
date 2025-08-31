namespace YourEcommerce.ViewModels;

public class ProductTagViewModel
{
    public int TagId { get; set; }
    public TagViewModel Tag { get; set; } = new TagViewModel();
}