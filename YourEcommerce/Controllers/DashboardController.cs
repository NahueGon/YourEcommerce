using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.DTOs.BrandDtos;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.DTOs.SportDtos;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IGenderService _genderService;
    private readonly IBrandService _brandService;
    private readonly ISportService _sportService;

    public DashboardController(
        ILogger<DashboardController> logger,
        IUserService userService,
        IProductService productService,
        ICategoryService categoryService,
        IGenderService genderService,
        IBrandService brandService,
        ISportService sportService
    )
    {
        _logger = logger;
        _userService = userService;
        _productService = productService;
        _categoryService = categoryService;
        _genderService = genderService;
        _brandService = brandService;
        _sportService = sportService;
    }

    [HttpGet("")]
    public ActionResult Index() => View();

    [HttpGet("products")]
    public async Task<ActionResult> Products()
    {
        var products = await _productService.GetAllFlat();
        var total = products.Count();

        return View(new TableDashboardViewModel<ProductDto>
        {
            Title = "Productos",
            Items = products,
            TotalItems = total
        });
    }

    [HttpPost("products/{id}/delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar el producto.");

        TempData["Success"] = "Producto eliminado correctamente.";
        return RedirectToAction(nameof(Products));
    }

    [HttpGet("users")]
    public async Task<ActionResult> Users()
    {
        var users = await _userService.GetAll();
        var total = users.Count();

        return View(new TableDashboardViewModel<UserViewModel>
        {
            Title = "Usuarios",
            Items = users,
            TotalItems = total
        });
    }

    [HttpPost("users/{id}/delete")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar el usuario.");

        TempData["Success"] = "Usuario eliminado correctamente.";
        return RedirectToAction(nameof(Users));
    }

    [HttpGet("categories")]
    public async Task<ActionResult> Categories()
    {
        var categories = await _categoryService.GetAll();
        var total = categories.Count();

        return View(new TableDashboardViewModel<CategoryDto>
        {
            Title = "Categorias",
            Items = categories,
            TotalItems = total
        });
    }

    [HttpPost("categories/{id}/delete")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var success = await _categoryService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar la categoria.");

        TempData["Success"] = "Categoria eliminada correctamente.";
        return RedirectToAction(nameof(Categories));
    }

    [HttpGet("genders")]
    public async Task<ActionResult> Genders()
    {
        var genders = await _genderService.GetAllFlat();
        var total = genders.Count();

        return View(new TableDashboardViewModel<GenderDto>
        {
            Title = "Generos",
            Items = genders,
            TotalItems = total
        });
    }

    [HttpPost("genders/{id}/delete")]
    public async Task<IActionResult> DeleteGender(int id)
    {
        var success = await _genderService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar el genero.");

        TempData["Success"] = "Genero eliminado correctamente.";
        return RedirectToAction(nameof(Genders));
    }

    [HttpGet("brands")]
    public async Task<ActionResult> Brands()
    {
        var brands = await _brandService.GetAllFlat();
        var total = brands.Count();

        return View(new TableDashboardViewModel<BrandDto>
        {
            Title = "Marcas",
            Items = brands,
            TotalItems = total
        });
    }

    [HttpPost("brands/{id}/delete")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var success = await _brandService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar la marca.");

        TempData["Success"] = "Marca eliminada correctamente.";
        return RedirectToAction(nameof(Brands));
    }

    [HttpGet("sports")]
    public async Task<ActionResult> Sports()
    {
        var sports = await _sportService.GetAllFlat();
        var total = sports.Count();

        return View(new TableDashboardViewModel<SportDto>
        {
            Title = "Deportes",
            Items = sports,
            TotalItems = total
        });
    }
    
    [HttpPost("sports/{id}/delete")]
    public async Task<IActionResult> DeleteSport(int id)
    {
        var success = await _sportService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar el deporte.");

        TempData["Success"] = "Deporte eliminado correctamente.";
        return RedirectToAction(nameof(Sports));
    }
}