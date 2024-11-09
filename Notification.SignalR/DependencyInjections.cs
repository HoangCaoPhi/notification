using Notification.SignalR.Context;

namespace Notification.SignalR;

public static class DependencyInjections
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddMongoDBClient("notificationdb");
        builder.Services.AddScoped<NotificationContext>();
        builder.Services.AddGrpc(options =>
        {
            if(builder.Environment.IsDevelopment())
            {
                options.EnableDetailedErrors = true;
            }
        });
    }
}
