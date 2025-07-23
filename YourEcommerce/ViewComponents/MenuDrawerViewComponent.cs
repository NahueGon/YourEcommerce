using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class MenuDrawerViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public MenuDrawerViewComponent(ICategoryService service)
    {
        _categoryService = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new MenuDrawerViewModel();
        var categories  = await _categoryService.GetCategoriesAsync();

        model.Categories = categories ?? new List<CategoryViewModel>();

        return View(model);
    }
}
