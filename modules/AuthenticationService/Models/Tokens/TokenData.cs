using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Models.Tokens;

public abstract class TokenData
{
    public string Issuer { get; }
    public string Audience { get; }
    public string Signature { get; }
    public double Lifetime { get; }
    public TokenValidationParameters ValidationParameters { get; }

    protected TokenData(AuthenticationServiceTokenConfig config) {
        Issuer = config.Issuer;
        Audience = config.Audience;
        Signature = config.Signature;
        Lifetime = config.Lifetime;
        ValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true, 
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Signature)),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    }
}