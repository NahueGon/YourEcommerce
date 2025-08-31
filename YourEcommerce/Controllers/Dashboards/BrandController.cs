using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Controllers.Dashboard;
using YourEcommerce.DTOs.BrandDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Controllers.Dashboards;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class BrandController : DashboardControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly IMapper _mapper;
    private readonly IBrandService _brandService;

    public BrandController(
        ILogger<BrandController> logger,
        IMapper mapper,
        IBrandService brandService,
        INotificationService notificationService
    ) : base(notificationService)
    {
        _logger = logger;
        _mapper = mapper;
        _brandService = brandService;
    }

    [HttpGet("brands")]
    public async Task<ActionResult> Index()
    {
        var brands = await _brandService.GetAllForTable();

        var model = new TableDashboardViewModel<BrandDto>
        {
            Title = "brands",
            Items = brands,
            TotalItems = brands.Count()
        };

        return DashboardView("Index", model);
    }

    [HttpGet("brands/create", Name = "CreateBrand")]
    public IActionResult CreateBrand()
    {
        var model = new BrandCreateDto();
        return DashboardView("CreateBrand", model);
    }

    [HttpPost("brands/create", Name = "StoreBrand")]
    public async Task<IActionResult> StoreBrand(BrandCreateDto brandDto)
    {
        if (!ModelState.IsValid) return DashboardView("CreateBrand", brandDto);

        var createdbrandDto = await _brandService.Create(brandDto);
        Notify(
            createdbrandDto != null,
            "Marca creada correctamente",
            "No se pudo crear la marca"
        );

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("brands/{id}/edit", Name = "EditBrand")]
    public async Task<IActionResult> EditBrand(int id)
    {
        var brand = await _brandService.GetForEdit(id);
        if (brand == null) return NotFound();

        var model = _mapper.Map<BrandUpdateDto>(brand);
        return DashboardView("EditBrand", model);
    }

    [HttpPost("brands/{id}/edit", Name = "UpdateBrand")]
    public async Task<IActionResult> UpdateBrand(int id, BrandUpdateDto brandDto)
    {
        if (!ModelState.IsValid) return DashboardView("EditBrand", brandDto);

        var updatedBrand = await _brandService.Update(id, brandDto);
        Notify(
            updatedBrand != null,
            "Marca actualizada correctamente",
            "No se pudo actualizar la marca"
        );

        return RedirectToAction("EditBrand", new { id = brandDto.Id });
    }

    [HttpPost("brands/{id}/delete", Name = "DeleteBrand")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var success = await _brandService.Delete(id);
        Notify(
            success,
            "Marca eliminada correctamente",
            "No se pudo borrar la marca"
        );

        return RedirectToAction(nameof(Index));
    }
}