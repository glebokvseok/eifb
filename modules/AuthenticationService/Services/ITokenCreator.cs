using System.Collections.Generic;
using System.Security.Claims;
using AuthenticationService.Models.Tokens;

namespace AuthenticationService.Services;

public interface ITokenCreator
{
    string CreateToken(TokenData data, IEnumerable<Claim> claims);
}