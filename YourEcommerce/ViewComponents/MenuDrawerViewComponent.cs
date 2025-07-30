using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

public class MenuDrawerViewComponent : ViewComponent
{
    private readonly IProductService _productService;

    public MenuDrawerViewComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new MenuDrawerViewModel();
        var products = await _productService.GetAllProducts();
        var allGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>();

        var genderCategoryTag = products
            .GroupBy(p => p.Gender)
            .ToDictionary(
                g => g.Key,
                g => g.GroupBy(p => p.Category.Name ?? "Sin categoría")
                    .ToDictionary(
                        c => c.Key,
                        c => c.SelectMany(p => p.ProductTags.Select(t => t.Tag.Name))
                                .Where(name => !string.IsNullOrEmpty(name))
                                .Distinct()
                                .ToList()
                    )
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
