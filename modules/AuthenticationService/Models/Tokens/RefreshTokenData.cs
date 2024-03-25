namespace AuthenticationService.Models.Tokens;

public class RefreshTokenData : TokenData
{
    public RefreshTokenData(AuthenticationServiceRefreshTokenConfig config) : base(config)
    {
    }
}