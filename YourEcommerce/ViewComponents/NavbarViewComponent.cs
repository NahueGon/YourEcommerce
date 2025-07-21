using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourEcommerce.ViewModels;

public class NavbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var user = HttpContext.User;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            var userName = user.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario";
            var email = user.FindFirst(ClaimTypes.Email)?.Value ?? "";
            int.TryParse(user.FindFirst(ClaimTypes.Role)?.Value, out int role);

            var model = new NavbarViewModel
            {
                Name = userName,
                Email = email,
                Role = role
            };

            return View(model);
        }

        return View(null);
    }
}
