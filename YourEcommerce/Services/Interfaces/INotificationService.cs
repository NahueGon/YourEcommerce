using Microsoft.AspNetCore.Mvc.ViewFeatures;
using YourEcommerce.Helpers;

namespace YourEcommerce.Services.Interfaces;

public interface INotificationService
{
    void Notify(ITempDataDictionary tempData, NotificationType type, string message);
}