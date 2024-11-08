using Microsoft.EntityFrameworkCore;
using Web.Server.Identity;

namespace Web.Server.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();

        dbContext.Database.Migrate();
    }
}
