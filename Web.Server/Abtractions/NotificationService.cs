using Microsoft.AspNetCore.Identity;
using Notification.Shared;
using System.Text.Json;
using Web.Server.Models;

namespace Web.Server.Abtractions;

public class NotificationService(
    IIdentityService identityService,
    UserManager<IdentityUser> userManager,
    Notification.Shared.Notification.NotificationClient notificationClient) : INotificationService
{
    public async Task SendAsync(NotificationRequest notificationRequest)
    {
        var notificationRecipient = await GetNotificationRecipient();
        ArgumentNullException.ThrowIfNull(notificationRecipient);

        var rawData = new
        {
            notificationRequest.Title,
            notificationRequest.PostId,
            SenderName = identityService.GetUserName()
        };
 
        var notification = new SendNotificationRequest
        {
            Action = "Post_Comment_Created",
            RawData = JsonSerializer.Serialize(rawData),
            Message = "##SenderName## already commented on your post titled ##Title##.",
            AppCode = "Notification",
            FromAppCode = "Notification",
            UserID = notificationRecipient.Id,
            SenderId = identityService.GetUserIdentity(),
            SenderName = identityService.GetUserName()
        };

        await notificationClient.SendNotificationAsync(notification);
    }

    private async Task<IdentityUser?> GetNotificationRecipient()
    {
        return await userManager.FindByNameAsync("test");
    }
}
