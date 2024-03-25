using AuthenticationService.Models.Tokens;

namespace AuthenticationService.Services;

public interface ITokenLifetimeManager
{
    double GetTimeBeforeExpiration(string token, TokenData data);
}