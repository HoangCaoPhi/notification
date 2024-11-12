using Web.Server;
using Web.Server.Apis;
using Web.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddApplicationServices();
 
var app = builder.Build();

app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.UseStaticFiles();

 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
    await app.ApplySeeding();
    app.UseCors("AllowNotificationDevOrigin");
}

 
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var routeGroup = app.MapGroup("/api"); 
routeGroup.MapNotificationApi();
routeGroup.MapAuthApi();

app.MapFallbackToFile("/index.html");

app.Run();
