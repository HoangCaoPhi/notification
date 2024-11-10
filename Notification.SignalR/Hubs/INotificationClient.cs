namespace Notification.SignalR.Hubs;

public interface INotificationClient
{
    Task ReceiveMessage(string userId, string message);
}
