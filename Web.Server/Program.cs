using Microsoft.AspNetCore.Identity;
using Web.Server;
using Web.Server.Apis;
using Web.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddApplicationServices();
 
builder.Services.AddAuthorization();

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
}

 
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var routeGroup = app.MapGroup("/api");

routeGroup.MapIdentityApi<IdentityUser>();
routeGroup.MapWebApi();

app.MapFallbackToFile("/index.html");

app.Run();
