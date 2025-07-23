using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class NavbarViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public NavbarViewComponent(ICategoryService service)
    {
        _categoryService = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavbarViewModel();
        var user = HttpContext.User;

        var categories  = await _categoryService.GetCategoriesAsync();
        model.Categories = categories ?? new List<CategoryViewModel>();

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            var userName = user.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario";
            var email = user.FindFirst(ClaimTypes.Email)?.Value ?? "";
            var role = user.FindFirst(ClaimTypes.Role)?.Value ?? "Customer";

            model.User = new UserViewModel
            {
                Name = userName,
                Email = email,
                Role = role
            };
        }

        return View(model);
    }
}
