using Microsoft.AspNetCore.Mvc;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class NavbarViewComponent : ViewComponent
{
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public NavbarViewComponent(IProductService productService, IUserService userService)
    {
        _productService = productService;
        _userService = userService;
    }

   public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavbarViewModel();
        
        var products = await _productService.GetAllForTable();

        var usedGenders = products
            .Where(p => p.Gender != null)
            .Select(p => new GenderDto
            {
                Id = p.Gender.Id,
                Name = p.Gender.Name
            })
            .DistinctBy(g => g.Id)
            .ToList();

        model.Genders = usedGenders;

        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        {
            var currentUser = await _userService.GetCurrent(HttpContext.User);
            
            model.User = currentUser ?? new UserResponseDto
            {
                Name = "Usuario",
                Role = UserRole.Customer,
                ProfileImage = "/img/anonymous-profile.png"
            };
        }
    
        return View(model);
    }
}