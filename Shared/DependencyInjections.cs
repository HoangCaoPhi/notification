using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared;
public static class DependencyInjections
{
    public static void AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            var corsOrigin = Environment.GetEnvironmentVariable("FRONTEND_DEV_URL");

            options.AddPolicy("AllowNotificationDevOrigin", policy =>
            {
                policy.WithOrigins(corsOrigin)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        builder.Services.ConfigureOptions<JwtOptionsSetup>();
        builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;            
        })
        .AddJwtBearer();

        builder.Services.AddAuthorization();

        builder.Services.AddScoped<IIdentityService, IdentityService>();
    }
 
}
