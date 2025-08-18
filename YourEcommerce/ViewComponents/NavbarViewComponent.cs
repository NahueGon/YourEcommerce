using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class NavbarViewComponent : ViewComponent
{
    private readonly IProductService _productService;
    private const string ApiBaseUrl = "http://192.168.100.11:5076/";

    public NavbarViewComponent(IProductService productService)
    {
        _productService = productService;
    }

   public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavbarViewModel();
        var user = HttpContext.User;
        var products = await _productService.GetAll();
        var allGenders = products
            .Select(p => p.Gender)      // ahora p.Gender es GenderViewModel
            .Where(g => g != null)
            .Distinct()
            .ToList();

        var usedGenders = products
            .Where(p => p.Gender != null)
            .Select(p => p.Gender!)
            .DistinctBy(g => g.Id)
            .ToList();

        model.Genders = usedGenders;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            model.User = new UserViewModel
            {
                Id = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"),
                Name = user.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario",
                Lastname = user.FindFirst(ClaimTypes.Surname)?.Value ?? "",
                Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "",
                Role = user.FindFirst(ClaimTypes.Role)?.Value ?? "Customer",
                ProfileImage = !string.IsNullOrEmpty(user.FindFirst("ProfileImage")?.Value)
                    ? ApiBaseUrl + user.FindFirst("ProfileImage")?.Value
                    : "",
            };
        }
    
        return View(model);
    }
}