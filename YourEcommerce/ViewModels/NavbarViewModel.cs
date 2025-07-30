namespace YourEcommerce.ViewModels;

public class NavbarViewModel
{
    public UserViewModel? User { get; set; }

    public List<Gender> Genders { get; set; } = new();
}
