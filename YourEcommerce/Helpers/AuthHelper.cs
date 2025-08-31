using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.Helpers;

public static class AuthHelper
{
    public static async Task RefreshUserClaims(HttpContext httpContext, UserResponseDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name ?? ""),
            new Claim(ClaimTypes.Surname, user.Lastname ?? ""),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""),
            new Claim(ClaimTypes.StreetAddress, user.Address ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role, (user.Role ?? UserRole.Customer).ToString()),
            new Claim("ProfileImage", user.ProfileImage ?? "")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }
}