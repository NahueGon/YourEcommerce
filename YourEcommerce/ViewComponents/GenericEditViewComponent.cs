using Microsoft.AspNetCore.Mvc;
using YourEcommerce.ViewModels;

namespace YourEcommerce.ViewComponents
{
    public class GenericEditViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic  item, string entityName)
        {
            var model = new GenericEditViewModel<dynamic>
            {
                Item = item,
                EntityName = entityName
            };
            model.OrderProperties();
            return View(model);
        }
    }
}