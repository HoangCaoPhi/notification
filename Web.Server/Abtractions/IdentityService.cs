using System.Security.Claims;

namespace Web.Server.Abtractions;

public class IdentityService(IHttpContextAccessor context) : IIdentityService
{
    public string? GetUserIdentity()
     => context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? GetUserName()
     => context.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
}
