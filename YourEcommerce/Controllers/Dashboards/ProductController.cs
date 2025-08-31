using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Controllers.Dashboard;
using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Controllers.Dashboards;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class ProductController : DashboardControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ProductController(
        ILogger<ProductController> logger,
        IMapper mapper,
        IProductService productService,
        INotificationService notificationService
    ) : base(notificationService)
    {
        _logger = logger;
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet("products")]
    public async Task<ActionResult> Index()
    {
        var products = await _productService.GetAllForTable();
        var total = products.Count();

        var model = new TableDashboardViewModel<ProductDto>
        {
            Title = "products",
            Items = products,
            TotalItems = total
        };

        return DashboardView("Index", model);
    }

    [HttpGet("products/{id}/edit", Name = "EditProduct")]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _productService.GetForEdit(id);
        if (product == null) return NotFound();

        var model = _mapper.Map<ProductUpdateDto>(product);

        ViewData["Title"] = "Editar Producto";
        
        return DashboardView("EditProduct", model);
    }

    [HttpPost("products/{id}/delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productService.Delete(id);
        if (!success) return BadRequest("No se pudo borrar el producto.");

        TempData["Success"] = "Producto eliminado correctamente.";
        return RedirectToAction(nameof(Index));
    }
}