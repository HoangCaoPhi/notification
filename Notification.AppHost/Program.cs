var builder = DistributedApplication.CreateBuilder(args);


var sql = builder
            .AddSqlServer("sql", port: 64086)
            .WithDataVolume();
var sqldb = sql.AddDatabase("identitydb");

var mongo = builder
            .AddMongoDB("mongo", port: 64294)
            .WithDataVolume();
var mongodb = mongo.AddDatabase("notificationdb");


var notificationServerSocket = builder.AddNpmApp(name: "notification-socket",
                                                 workingDirectory: "../Notification.SocketIO",
                                                 scriptName: "start")
                                    .WithHttpEndpoint(env: "PORT")
                                    .WithExternalHttpEndpoints()
                                    .PublishAsDockerFile();

var notification = builder
    .AddProject<Projects.Notification_SignalR>("notification-api")
    .WithReference(mongodb)
    .WithReference(notificationServerSocket);

builder
    .AddProject<Projects.Web_Server>("web-server")
    .WithReference(sqldb)
    .WithReference(notification);


builder.Build().Run();
