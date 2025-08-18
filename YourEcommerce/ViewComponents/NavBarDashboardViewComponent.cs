using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class NavBarDashboardViewComponent : ViewComponent
{
    private readonly IUserService _userService;
    private const string ApiBaseUrl = "http://192.168.100.11:5076/";

    public NavBarDashboardViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsPrincipal = User as ClaimsPrincipal;

        var userDto = claimsPrincipal != null 
            ? await _userService.GetCurrent(claimsPrincipal)
            : null;

        var model = new NavbarDashboardViewModel
        {
            User = new UserViewModel
            {
                Id = userDto?.Id ?? 0,
                Name = userDto?.Name ?? "Usuario",
                Lastname = userDto?.Lastname ?? "",
                Email = userDto?.Email ?? "",
                Role = userDto?.Role ?? "Customer",
                ProfileImage = !string.IsNullOrEmpty(userDto?.ProfileImage)
                    ? ApiBaseUrl + userDto.ProfileImage
                    : "/img/anonymous-profile.png"
            }
        };

        return View(model);
    }
}