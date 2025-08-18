using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Models;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;
using YourEcommerce.Helpers;

namespace YourEcommerce.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService, IUserService userService)
    {
        _logger = logger;
        _authService = authService;
        _userService = userService;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel loginModel)
    {
        if (!ModelState.IsValid) return View(loginModel);

        string? token = await _authService.LoginAsync(loginModel);

        if (string.IsNullOrEmpty(token))
        {
            ViewBag.ErrorToast = "Credenciales inválidas";
            return View(loginModel);
        }

        HttpContext.Session.SetString("AuthToken", token);

        var user = await _userService.GetByEmail(loginModel.Email);

        if (user == null)
        {
            ViewBag.ErrorToast = "No se pudieron obtener los datos del usuario.";
            return View(loginModel);
        }

        await AuthHelper.RefreshUserClaims(HttpContext, user);

        ViewBag.ShowSuccess = true;
        ViewBag.UserData = user;

        return View(loginModel);
    }


    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterViewModel registerModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(registerModel);

        if (result == null)
        {
            ViewBag.ErrorToast = "No se pudo registrar el usuario. El email ya existe.";
            return View(registerModel);
        }

        ViewBag.ShowSuccess = true;
        ModelState.Clear();

        return View(registerModel);
    }

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        TempData["ErrorMessage"] = "No tienes permisos para acceder a esa página.";
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
