namespace AuthenticationService.Models.Tokens;

public class AccessTokenData : TokenData
{
    public AccessTokenData(AuthenticationServiceAccessTokenConfig config) : base(config)
    {
    }
}