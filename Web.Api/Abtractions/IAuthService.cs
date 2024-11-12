using Web.Server.Models;

namespace Web.Server.Abtractions;

public interface IAuthService
{
    Task<IResult> Register(RegisterModel registerModel);

    Task<IResult> Login(LoginModel loginModel);
}
