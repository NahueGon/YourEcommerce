using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.ViewModels;

public class NavbarViewModel
{
    public UserViewModel? User { get; set; }

    public List<GenderViewModel> Genders { get; set; } = new();
}