using Grpc.Core;
using Notification.Shared;
using Notification.SignalR.Context;
using System.Text.Json;
using static Notification.Shared.Notification;

namespace Notification.SignalR.Services
{
    public class NotificationService(NotificationContext notificationContext) : NotificationBase
    {
        private readonly NotificationContext _notificationContext = notificationContext;
 
        public override async Task<SendNotificationReply> SendNotification(SendNotificationRequest request, ServerCallContext context)
        {
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

            await _notificationContext.Notifications.InsertOneAsync(notification);

            return new SendNotificationReply
            {
                Success = true,
                Data = JsonSerializer.Serialize(notification)
            };
        }
    }
}
