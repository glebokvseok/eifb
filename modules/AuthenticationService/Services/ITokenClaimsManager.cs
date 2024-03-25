using System.Collections.Generic;
using System.Security.Claims;
using AuthenticationService.Models.Tokens;

namespace AuthenticationService.Services;

public interface ITokenClaimsManager
{
    IEnumerable<Claim> GetClaims(string token, TokenData data);
}