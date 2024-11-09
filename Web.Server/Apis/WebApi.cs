using Microsoft.AspNetCore.Mvc;
using Web.Server.Abtractions;
using Web.Server.Models;

namespace Web.Server.Apis;

public static class WebApi
{
    public static IEndpointRouteBuilder MapWebApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/notification", SendNotification)
           .RequireAuthorization();
        return app;
    }
 
    public static async Task SendNotification(
        [FromBody] NotificationRequest notificationRequest,
        INotificationService notificationService)
    {
        await notificationService.SendAsync(notificationRequest);
    }

 
}
