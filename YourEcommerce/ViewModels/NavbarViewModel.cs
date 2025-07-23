namespace YourEcommerce.ViewModels;

public class NavbarViewModel
{
    public UserViewModel? User { get; set; }
    public List<CategoryViewModel> Categories { get; set; } = new();
}
