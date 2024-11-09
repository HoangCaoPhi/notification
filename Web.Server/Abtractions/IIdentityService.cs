namespace Web.Server.Abtractions;

public interface IIdentityService
{
    string? GetUserIdentity();

    string? GetUserName();
}
