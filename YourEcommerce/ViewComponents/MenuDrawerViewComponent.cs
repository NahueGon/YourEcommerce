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
        var products = await _productService.GetAllForTable();

        var uniqueGenders = products
            .Where(p => p.Gender != null)
            .Select(p => p.Gender!)
            .GroupBy(g => g.Id)
            .Select(g => g.First())
            .ToList();

        foreach (var gender in uniqueGenders)
        {
            var productsOfGender = products.Where(p => p.Gender?.Id == gender.Id).ToList();

            var categoryDict = new Dictionary<string, List<string>>();

            var categories = productsOfGender
                .Where(p => p.Category != null)
                .GroupBy(p => p.Category!.Name);

            foreach (var categoryGroup in categories)
            {
                var categoryName = categoryGroup.Key;

                var tags = categoryGroup
                    .SelectMany(p => p.ProductTags.Select(t => t.Tag.Name))
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .Distinct()
                    .ToList();

                categoryDict[categoryName] = tags;
            }

            var sports = productsOfGender
                .Where(p => p.Sport != null && !string.IsNullOrWhiteSpace(p.Sport.Name))
                .Select(p => p.Sport!.Name!)
                .Distinct()
                .ToList();

            if (sports.Any())
            {
                categoryDict["Deportes"] = sports;
            }

            model.MenuStructure[gender] = categoryDict;
        }

        return View(model);
    }
}