namespace Notification.SignalR;

public static class DependencyInjections
{
    public static void AddMongoDb(this IHostApplicationBuilder builder)
    {
        builder.AddMongoDBClient("notificationdb");
    }
}
