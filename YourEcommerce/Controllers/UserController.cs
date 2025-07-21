using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService service)
    {
        _logger = logger;
        _userService = service;
    }

    public ActionResult Index()
    {
        return View();
    }

}

