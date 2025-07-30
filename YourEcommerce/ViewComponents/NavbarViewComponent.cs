using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class NavbarViewComponent : ViewComponent
{
    private readonly IProductService _productService;

    public NavbarViewComponent(IProductService productService)
    {
        _productService = productService;
    }

   public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavbarViewModel();
        var user = HttpContext.User;
        var products = await _productService.GetAllProducts();
        var allGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>();

        var usedGenders = products.Select(p => p.Gender).Distinct().ToList();

        model.Genders = usedGenders;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            model.User = new UserViewModel
            {
                Name = user.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario",
                Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "",
                Role = user.FindFirst(ClaimTypes.Role)?.Value ?? "Customer"
            };
        }
    
        return View(model);
    }
}