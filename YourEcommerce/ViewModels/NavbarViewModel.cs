using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.ViewModels;

public class NavbarViewModel
{
    public UserResponseDto? User { get; set; }

    public List<GenderDto> Genders { get; set; } = new();
}