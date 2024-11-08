using Grpc.Core;
using Notification.SignalR.Protos;
using static Notification.SignalR.Protos.Notification;

namespace Notification.SignalR.Services;

public class NotificationService : NotificationBase
{
    public override Task<NotificationReply> SendNotification(NotificationRequest request, ServerCallContext context)
    {
        return base.SendNotification(request, context);
    }
}
