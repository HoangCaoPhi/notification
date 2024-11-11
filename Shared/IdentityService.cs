using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared;

public class IdentityService(IHttpContextAccessor context) : IIdentityService
{
    public string? GetUserIdentity()
     => context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? GetUserName()
     => context.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
}
