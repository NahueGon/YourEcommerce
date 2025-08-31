using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YourEcommerce.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(
        ILogger<DashboardController> logger
    ) {
        _logger = logger;
    }

    [HttpGet("")]
    public ActionResult Index() => View();
}