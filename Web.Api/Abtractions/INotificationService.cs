using Web.Server.Models;

namespace Web.Server.Abtractions;

public interface INotificationService
{
    Task SendAsync(NotificationRequest notificationRequest);
}
