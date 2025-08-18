using Microsoft.AspNetCore.Mvc;
using YourEcommerce.ViewModels;

public class TableDashboardViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string title, IEnumerable<dynamic> items, int total, string url)
    {
        var model = new TableDashboardViewModel<dynamic>
        {
            Title = title,
            Items = items,
            TotalItems = total,
            ApiBaseUrl = url
        };

        return View(model);
    }
}