using Notification.SignalR.Context;
using Shared;

namespace Notification.SignalR;

public static class DependencyInjections
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddMongoDBClient("notificationdb");
        builder.Services.AddScoped<NotificationContext>();

        builder.AddDefaultAuthentication();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddGrpc(options =>
        {
            if(builder.Environment.IsDevelopment())
            {
                options.EnableDetailedErrors = true;
            }
        });

        builder.Services.AddSignalR(); 
    }
}
