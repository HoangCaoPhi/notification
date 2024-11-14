using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Net.Http.Headers;
using Shared;
using System.Net.Http.Headers;
using Web.Server.Abtractions;
using Web.Server.Identity;

namespace Web.Server;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddDefaultAuthentication();

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        builder.AddSqlServerDbContext<IdentityContext>("identitydb");

        builder.Services.AddHttpContextAccessor(); 
        builder.Services.AddProblemDetails();

        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<ITokenProvider, TokenProvider>();    
        builder.Services.AddScoped<IAuthService, AuthService>();

        // For SignalR
        //builder.Services.AddGrpcClient<Notification.Shared.Notification.NotificationClient>
        //                    (o => o.Address = new("https://notification-api"))
        //                    .AddAuthToken();

        // For socket 
        builder.Services.AddGrpcClient<Notification.Shared.Notification.NotificationClient>
            (o => o.Address = new(builder.Configuration["Socket_Url"]!))
            .AddAuthToken();
    }

    public static IHttpClientBuilder AddAuthToken(this IHttpClientBuilder builder)
    {
        builder.Services.TryAddTransient<HttpClientAuthorizationDelegatingHandler>();

        builder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        return builder;
    }

    private class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext is HttpContext context)
            {
                var accessToken = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (accessToken is not null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
