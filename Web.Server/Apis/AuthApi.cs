using Web.Server.Abtractions;
using Web.Server.Models;

namespace Web.Server.Apis;

public static class AuthApi
{
    public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", Register);
        app.MapPost("/login", Login);

        return app;
    }
 
    public static async Task<IResult> Register(
         IAuthService authService,
         RegisterModel registerModel)
    {
         return await authService.Register(registerModel);
    }

    public static async Task<IResult> Login(IAuthService authService, LoginModel loginModel)
    {
        return await authService.Login(loginModel);
    }
}
