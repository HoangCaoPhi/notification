using Notification.SignalR;
using Notification.SignalR.Hubs;
using Notification.SignalR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDevSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:5173") 
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
    app.UseCors("AllowDevSpecificOrigin");
}

app.MapGet("test", () => "Test").RequireAuthorization();
 
app.UseHttpsRedirection();

app.MapHub<NotificationHub>("/notificationHub");
 
app.Run();
 