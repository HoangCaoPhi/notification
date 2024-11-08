using MongoDB.Driver;

namespace Notification.SignalR.Context;

public class NotificationContext(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("notificationdb");

    public IMongoCollection<Models.Notification> Notifications
    {
        get
        {
            return _database.GetCollection<Models.Notification>("Notifications");
        }
    }
}
