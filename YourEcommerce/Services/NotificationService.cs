using Microsoft.AspNetCore.Mvc.ViewFeatures;
using YourEcommerce.Helpers;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class NotificationService : INotificationService
{
    public void Notify(ITempDataDictionary tempData, NotificationType type, string message)
        {
            tempData["Notification.Type"] = type.ToString().ToLower();
            tempData["Notification.Message"] = message;
        }
}