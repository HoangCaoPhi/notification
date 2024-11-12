using Notification.SignalR;
using Notification.SignalR.Hubs;
using Notification.SignalR.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints(); 
app.MapGrpcService<NotificationService>();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowNotificationDevOrigin");
}

app.MapGet("test", (HttpContext context) =>
{
    var nameIdentifier = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}).RequireAuthorization();
 
app.UseHttpsRedirection();

app.MapHub<NotificationHub>("/notificationHub");
  
app.Run();
 