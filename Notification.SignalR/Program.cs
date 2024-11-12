using Notification.SignalR;
using Notification.SignalR.Hubs;
using Notification.SignalR.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(o => o.WithOrigins("https://localhost:5173")
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                                   .AllowCredentials());
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints(); 
app.MapGrpcService<NotificationService>();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.MapGet("test", (HttpContext context) =>
{
    var nameIdentifier = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}).RequireAuthorization();
 
app.UseHttpsRedirection();

app.MapHub<NotificationHub>("/notificationHub");
  
app.Run();
 