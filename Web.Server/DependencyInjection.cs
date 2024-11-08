using Microsoft.AspNetCore.Identity;
using Web.Server.Identity;

namespace Web.Server;

public static class DependencyInjection
{
    public static void AddIdentityContext(this IHostApplicationBuilder builder)
    {
        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<IdentityContext>();
      
        builder.AddSqlServerDbContext<IdentityContext>("identitydb");
    }
}
