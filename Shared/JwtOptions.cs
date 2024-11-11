namespace Shared;
public class JwtOptions
{
    public const string Section = "Jwt";
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
    public int AccessTokenValidityInMinutes { get; init; } 
}
