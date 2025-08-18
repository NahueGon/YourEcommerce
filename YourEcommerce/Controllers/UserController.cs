using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Helpers;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Controllers;

[Route("user")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private const string ApiBaseUrl = "http://192.168.100.11:5076/";

    public UserController(ILogger<UserController> logger, IUserService service)
    {
        _logger = logger;
        _userService = service;
    }

    [Authorize]
    [HttpGet("profile/{id}")]
    public async Task<IActionResult> Profile(int id)
    {
        var user = await _userService.Get(id);
        if (user == null) return RedirectToAction("Login", "Auth");

        var model = MapToUserUpdateViewModel(user);
        model.ProfileImageUrl = GetFullProfileImageUrl(user.ProfileImage);

        var hasPasswordErrors = ModelState["CurrentPassword"]?.Errors.Count > 0
                                || ModelState["NewPassword"]?.Errors.Count > 0;
        ViewBag.ShowPasswordForm = hasPasswordErrors;

        return View(model);
    }

    [Authorize]
    [HttpPost("profile/{id}")]
    public async Task<IActionResult> Profile(int id, UserUpdateViewModel userDto)
    {
        var updatedUser = await _userService.Update(id, userDto);

        if (updatedUser == null)
        {
            ModelState.AddModelError(nameof(userDto.CurrentPassword), "La contraseña actual es incorrecta.");

            var user = await _userService.Get(id);
            if (user == null) return NotFound();

            userDto.ProfileImageUrl = GetFullProfileImageUrl(user.ProfileImage);
            CompleteUserDtoWithExistingData(userDto, user);

            ViewBag.ShowPasswordForm = true;

            return View(userDto);
        }

        await AuthHelper.RefreshUserClaims(HttpContext, updatedUser);

        TempData["ShowSuccess"] = true;
        TempData["SuccessMessage"] = "Perfil actualizado correctamente";

        return RedirectToAction("Profile", new { id = userDto.Id });
    }

    private static UserUpdateViewModel MapToUserUpdateViewModel(UserViewModel user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Lastname = user.Lastname,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
        Address = user.Address
    };

    private static void CompleteUserDtoWithExistingData(UserUpdateViewModel userDto, UserViewModel user)
    {
        userDto.Name = user.Name;
        userDto.Lastname = user.Lastname;
        userDto.Email = user.Email;
        userDto.PhoneNumber = user.PhoneNumber;
        userDto.Address = user.Address;
    }

    private static string GetFullProfileImageUrl(string? profileImagePath)
    {
        if (string.IsNullOrEmpty(profileImagePath)) return "/img/anonymous-profile.png";

        return ApiBaseUrl + profileImagePath;
    }
}