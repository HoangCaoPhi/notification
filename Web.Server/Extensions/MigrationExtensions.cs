using Microsoft.EntityFrameworkCore;
using Web.Server.Identity;

namespace Web.Server.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
 
            dbContext.Database.Migrate();
        }
        catch (Exception ex) { 
            Console.WriteLine($"Migration failed: {ex.Message}");
        }
    }
}
