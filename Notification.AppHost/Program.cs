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

var notificationApi = builder
    .AddProject<Projects.Notification_SignalR>("notification-api")
    .WithReference(mongodb)
    .WithReference(notificationServerSocket);

var webApi = builder
            .AddProject<Projects.Web_Api>("web-api")
            .WithReference(sqldb)
            .WithReference(notificationApi);

var webFrontend = builder.AddNpmApp(name: "web-frontend",
                                    workingDirectory: "../web.frontend",
                                    scriptName: "dev")
                          .WithHttpEndpoint(env: "FE_PORT")
                          .WithExternalHttpEndpoints()
                          .PublishAsDockerFile();


var webApiendpoint = webApi.GetEndpoint("https");
webFrontend.WithEnvironment("VITE_API", webApiendpoint);

var webFrontendUrl = webFrontend.GetEndpoint("http");
webApi.WithEnvironment("FRONTEND_DEV_URL", webFrontendUrl);
notificationApi.WithEnvironment("FRONTEND_DEV_URL", webFrontendUrl);

builder.Build().Run();
