using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationService.Models.Tokens;

namespace AuthenticationService.Services;

public class TokenDataManager : ITokenClaimsManager, ITokenLifetimeManager
{
    public IEnumerable<Claim> GetClaims(string token, TokenData data) 
    {
        return new JwtSecurityTokenHandler().ValidateToken(token, data.ValidationParameters, out _).Claims;
    }

    public double GetTimeBeforeExpiration(string token, TokenData data) 
    {
        new JwtSecurityTokenHandler().ValidateToken(token, data.ValidationParameters, out var validatedToken);
        return (validatedToken.ValidTo.ToUniversalTime() - DateTime.Now.ToUniversalTime()).TotalMinutes;
    }
}