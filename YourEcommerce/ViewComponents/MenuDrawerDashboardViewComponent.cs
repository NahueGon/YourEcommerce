using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class MenuDrawerDashboardViewComponent : ViewComponent
{
    private readonly IProductService _productService;

    public MenuDrawerDashboardViewComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new MenuDrawerViewModel();
        var products = await _productService.GetAll();
        var allGenders = products
            .Select(p => p.Gender)      // ahora p.Gender es GenderViewModel
            .Where(g => g != null)
            .Distinct()
            .ToList();

        var genderCategoryTag = products
            .GroupBy(p => p.Gender)
            .ToDictionary(
                genderGroup => genderGroup.Key,
                genderGroup =>
                {
                    var categoryDict = genderGroup
                        .GroupBy(p => p.Category?.Name ?? "Sin categoría")
                        .ToDictionary(
                            categoryGroup => categoryGroup.Key,
                            categoryGroup =>
                            {
                                return categoryGroup
                                    .SelectMany(p => p.ProductTags.Select(t => t.Tag.Name))
                                    .Where(name => !string.IsNullOrWhiteSpace(name))
                                    .Select(name => name!)
                                    .Distinct()
                                    .ToList();
                            }
                        );

                    var sports = genderGroup
                        .Where(p => p.Sport != null && !string.IsNullOrWhiteSpace(p.Sport.Name))
                        .Select(p => p.Sport!.Name!)
                        .Distinct()
                        .ToList();

                    if (sports.Any())
                    {
                        categoryDict["Deportes"] = sports;
                    }

                    return categoryDict;
                }
            );

        foreach (var gender in allGenders)
        {
            if (!genderCategoryTag.ContainsKey(gender))
            {
                genderCategoryTag[gender] = new Dictionary<string, List<string>>();
            }
        }

        model.MenuStructure = genderCategoryTag;
        return View(model);
    }
}
