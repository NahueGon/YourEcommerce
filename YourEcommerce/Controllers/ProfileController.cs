using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Helpers;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Controllers;

[Route("user")]
public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public ProfileController(ILogger<ProfileController> logger, IMapper mapper, IUserService service)
    {
        _logger = logger;
        _userService = service;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("profile/{id}")]
    public async Task<IActionResult> Index(int id)
    {
        var user = await _userService.Get(id);
        if (user == null) return RedirectToAction("Login", "Auth");

        var model = _mapper.Map<UserUpdateDto>(user);
        model.ProfileImageUrl = user.ProfileImage;

        var hasPasswordErrors = ModelState["CurrentPassword"]?.Errors.Count > 0
                                || ModelState["NewPassword"]?.Errors.Count > 0;
        ViewBag.ShowPasswordForm = hasPasswordErrors;

        return View(model);
    }

    [Authorize]
    [HttpPost("profile/{id}")]
    public async Task<IActionResult> Edit(int id, UserUpdateDto userDto)
    {
        var updatedUser = await _userService.Update(id, userDto);

        if (updatedUser == null)
        {
            ModelState.AddModelError(nameof(userDto.CurrentPassword), "La contraseña actual es incorrecta.");

            var user = await _userService.Get(id);
            if (user == null) return NotFound();

            userDto = _mapper.Map<UserUpdateDto>(user);
            userDto.ProfileImageUrl = user.ProfileImage;

            ViewBag.ShowPasswordForm = true;

            return View(userDto);
        }
        
        var userResponse = _mapper.Map<UserResponseDto>(updatedUser);
        await AuthHelper.RefreshUserClaims(HttpContext, userResponse);

        TempData["ShowSuccess"] = true;
        TempData["SuccessMessage"] = "Perfil actualizado correctamente";

        return RedirectToAction("Index", new { id = userDto.Id });
    }
}