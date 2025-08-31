using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Helpers;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Controllers.Dashboard
{
    public abstract class DashboardControllerBase : Controller
    {
        protected readonly INotificationService _notificationService;

        protected DashboardControllerBase(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        protected ViewResult DashboardView(string viewName, object? model = null)
        {
            var controllerName = this.GetType().Name.Replace("Controller", "");
            var viewPath = $"~/Views/Dashboard/{controllerName}/{viewName}.cshtml";

            return View(viewPath, model);
        }

        protected void Notify(bool success, string successMessage, string errorMessage)
        {
            _notificationService.Notify(
                TempData,
                success ? NotificationType.Success : NotificationType.Error,
                success ? successMessage : errorMessage
            );
        }
    }
}