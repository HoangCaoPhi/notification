using Microsoft.AspNetCore.Identity;
using Web.Server.Identity;
using Web.Server.Models;

namespace Web.Server.Abtractions;

public class AuthService(UserManager<IdentityUser> userManager,
                         SignInManager<IdentityUser> signInManager,
                         ITokenProvider tokenProvider) : IAuthService
{
    public async Task<IResult> Register(RegisterModel registerModel)
    {
        var user = new IdentityUser { UserName = registerModel.Username, Email = registerModel.Email };
        var result = await userManager.CreateAsync(user, registerModel.Password);

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors);

        return Results.Ok(new { Message = "User registered successfully!" });
    }

    public async Task<IResult> Login(LoginModel loginModel)
    {
        var user = await userManager.FindByNameAsync(loginModel.Username);
        if (user is null)
            return Results.Unauthorized();

        var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
        if (!result.Succeeded)
            return Results.Unauthorized();

        var token = tokenProvider.GenerateJwtToken(user);
        return Results.Ok(new { AccessToken = token });
    }
}
