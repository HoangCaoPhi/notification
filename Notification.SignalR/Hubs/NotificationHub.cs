using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Notification.SignalR.Hubs;

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("Client is connected");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Client is disconnected");
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendNotification(string userId, string message)
        => await Clients.User(userId).ReceiveMessage(userId, message);
}
