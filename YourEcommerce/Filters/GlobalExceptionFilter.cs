using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using YourEcommerce.Helpers;
using YourEcommerce.Services.Interfaces;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<GlobalExceptionFilter> _logger;
    private readonly ITempDataDictionaryFactory _tempDataFactory;

    public GlobalExceptionFilter(
        INotificationService notificationService,
        ILogger<GlobalExceptionFilter> logger,
        ITempDataDictionaryFactory tempDataFactory)
    {
        _notificationService = notificationService;
        _logger = logger;
        _tempDataFactory = tempDataFactory;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocurrió un error inesperado");

        var tempData = _tempDataFactory.GetTempData(context.HttpContext);

        _notificationService.Notify(tempData, NotificationType.Error, "Ocurrió un error inesperado");

        var routeValues = context.RouteData.Values;
        var currentController = routeValues["controller"]?.ToString()?.Replace("Controller", "");
        context.Result = new RedirectToActionResult("Index", currentController, null);

        context.ExceptionHandled = true;
    }
}