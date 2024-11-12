using Microsoft.AspNetCore.Identity;

namespace Web.Server.Extensions;

public static class SeederExtensions
{
    private static async Task EnsureRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var existingRole = await roleManager.RoleExistsAsync("User");
        if (!existingRole)
        {
            await roleManager.CreateAsync(new IdentityRole { Name = "User" });
        } 
    }

    private static async Task EnsureUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var aliceUser = await userManager.FindByEmailAsync("alice@gmail.com");

        if (aliceUser == null)
        {
            var newUser = new IdentityUser()
            {
                Email = "alice@gmail.com",
                UserName = "alice",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newUser, "12345678@Abc");
            await userManager.AddToRolesAsync(newUser, ["User"]);
        }

        var bobUser = await userManager.FindByEmailAsync("bob@gmail.com");

        if (bobUser == null)
        {
            var newUser = new IdentityUser()
            {
                Email = "bob@gmail.com",
                UserName = "bob",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newUser, "12345678@Abc");
            await userManager.AddToRolesAsync(newUser, ["User"]);
        }

        var testUser = await userManager.FindByEmailAsync("test@gmail.com");

        if (bobUser == null)
        {
            var newUser = new IdentityUser()
            {
                Email = "test@gmail.com",
                UserName = "test",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newUser, "12345678@Abc");
            await userManager.AddToRolesAsync(newUser, ["User"]);
        }
    }

    public static async Task ApplySeeding(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await EnsureRoles(serviceProvider);
        await EnsureUsers(serviceProvider);
    }

}
