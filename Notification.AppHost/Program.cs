var builder = DistributedApplication.CreateBuilder(args);


var sql = builder
            .AddSqlServer("sql", port: 64086)
            .WithDataVolume();
var sqldb = sql.AddDatabase("identitydb");

var mongo = builder
            .AddMongoDB("mongo", port: 64294)
            .WithDataVolume();
var mongodb = mongo.AddDatabase("notificationdb");

builder
    .AddProject<Projects.Web_Server>("web-server")
    .WithReference(sqldb);

builder
    .AddProject<Projects.Notification_SignalR>("signalR")
    .WithReference(mongodb);

builder.AddProject<Projects.web_client>("web-client");
 

builder.Build().Run();
