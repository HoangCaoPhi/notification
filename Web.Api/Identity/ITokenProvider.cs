using Microsoft.AspNetCore.Identity;

namespace Web.Server.Identity;

public interface ITokenProvider
{
    string GenerateJwtToken(IdentityUser user);
}

 