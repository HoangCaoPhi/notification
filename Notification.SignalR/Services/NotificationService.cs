using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using Notification.Shared;
using Notification.SignalR.Context;
using Notification.SignalR.Hubs;
using System.Text.Json;
using static Notification.Shared.Notification;

namespace Notification.SignalR.Services
{
    public class NotificationService(
        NotificationContext notificationContext,
        IHubContext<NotificationHub, INotificationClient> hubContext) : NotificationBase
    { 
        public override async Task<SendNotificationReply> SendNotification(SendNotificationRequest request, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;
            var userId = user.Identity?.Name;

            var notification = new Models.Notification
            {
                Action = request.Action,
                GroupAction = request.GroupAction,
                RawData = request.RawData,
                GroupID = request.GroupId,
                AppCode = request.AppCode,
                FromAppCode = request.FromAppCode,
                Message = request.Message,
                SenderID = request.SenderId,
                SenderName = request.SenderName,
                CreatedAt = DateTime.UtcNow,
                IsReaded = false
            };

            await notificationContext.Notifications.InsertOneAsync(notification);

            await hubContext.Clients.All.ReceiveMessage("test", JsonSerializer.Serialize(notification));

            return new SendNotificationReply
            {
                Success = true,
                Data = JsonSerializer.Serialize(notification)
            };
        }
    }
}
