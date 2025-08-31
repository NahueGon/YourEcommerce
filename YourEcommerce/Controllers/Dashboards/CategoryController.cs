using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Controllers.Dashboard;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Controllers.Dashboards;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class CategoryController : DashboardControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;

    public CategoryController(
        ILogger<CategoryController> logger,
        IMapper mapper,
        ICategoryService categoryService,
        INotificationService notificationService
    ) : base(notificationService)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    [HttpGet("categories")]
    public async Task<ActionResult> Index()
    {
        var categories = await _categoryService.GetAllForTable();

        var model = new TableDashboardViewModel<CategoryDto>
        {
            Title = "categories",
            Items = categories,
            TotalItems = categories.Count()
        };

        return DashboardView("Index", model);
    }

    [HttpGet("categories/create", Name = "CreateCategory")]
    public IActionResult CreateCategory()
    {
        var model = new CategoryCreateDto();
        return DashboardView("CreateCategory", model);
    }

    [HttpPost("categories/create", Name = "StoreCategory")]
    public async Task<IActionResult> StoreCategory(CategoryCreateDto categoryDto)
    {
        if (!ModelState.IsValid) return DashboardView("CreateCategory", categoryDto);

        var createdCategory = await _categoryService.Create(categoryDto);
        Notify(
            createdCategory != null,
            "Categoria creada correctamente",
            "No se pudo crear la categoria"
        );

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("categories/{id}/edit", Name = "EditCategory")]
    public async Task<IActionResult> EditCategory(int id)
    {
        var category = await _categoryService.GetForEdit(id);
        if (category == null) return NotFound();

        var model = _mapper.Map<CategoryUpdateDto>(category);
        return DashboardView("EditCategory", model);
    }

    [HttpPost("categories/{id}/edit", Name = "UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto categoryDto)
    {
        if (!ModelState.IsValid) return DashboardView("EditCategory", categoryDto);

        var updatedCategory = await _categoryService.Update(id, categoryDto);
        Notify(
            updatedCategory != null,
            "Categoria actualizado correctamente",
            "No se pudo actualizar la categoria"
        );

        return RedirectToAction("EditCategory", new { id = categoryDto.Id });
    }

    [HttpPost("categories/{id}/delete", Name = "DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var success = await _categoryService.Delete(id);
        Notify(
            success,
            "Categoria eliminado correctamente",
            "No se pudo borrar la categoria"
        );

        return RedirectToAction(nameof(Index));
    }
}