namespace AuthenticationService.Models.Tokens;

public abstract class AuthenticationServiceTokenConfig
{
    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string Signature { get; set; } = null!;
    
    public double Lifetime { get; set; }
}