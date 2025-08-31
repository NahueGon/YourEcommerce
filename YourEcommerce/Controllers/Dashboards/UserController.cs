using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Controllers.Dashboard;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Helpers;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Controllers.Dashboards;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class UserController : DashboardControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger,
        IMapper mapper, 
        IUserService userService, 
        INotificationService notificationService
    ) : base(notificationService)
    {
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
    }
    
    [HttpGet("users")]
    public async Task<ActionResult> Index()
    {
        var users = await _userService.GetAllForTable();
        var total = users.Count;

        var model = new TableDashboardViewModel<UserDto>
        {
            Title = "users",
            Items = users,
            TotalItems = total
        };

        return DashboardView("Index", model);
    }

    [HttpGet("users/{id}/edit", Name = "EditUser")]
    public async Task<IActionResult> EditUser(int id)
    {
        var user = await _userService.Get(id);
        if (user == null) return NotFound();

        var model = _mapper.Map<UserUpdateDto>(user);

        return DashboardView("EditUser", model);
    }

    [HttpPost("users/{id}/edit", Name = "UpdateUser")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
    {
        var updatedUser = await _userService.Update(id, userDto);

        if (updatedUser == null)
        {
            var user = await _userService.Get(id);
            if (user == null) return NotFound();

            userDto = _mapper.Map<UserUpdateDto>(user);
            userDto.ProfileImageUrl = user.ProfileImage;

            return View(userDto);
        }

        var userResponse = _mapper.Map<UserResponseDto>(updatedUser);
        await AuthHelper.RefreshUserClaims(HttpContext, userResponse);
        
        TempData["ShowSuccess"] = true;
        TempData["SuccessMessage"] = "Perfil actualizado correctamente";

        return RedirectToAction("EditUser", new { id = userDto.Id });
    }

    [HttpPost("users/{id}/delete")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar el usuario.");

        TempData["Success"] = "Usuario eliminado correctamente.";
        return RedirectToAction(nameof(Index));
    }
}