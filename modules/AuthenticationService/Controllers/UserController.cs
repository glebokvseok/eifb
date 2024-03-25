using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AuthenticationService.Models;
using AuthenticationService.Models.Tokens;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
    private const int NumberOfRequiredClaims = 2;
    
    private readonly ITokenCreator _tokenCreator;
    private readonly ITokenClaimsManager _tokenClaimsManager;
    private readonly ITokenLifetimeManager _tokenLifetimeManager;
    private readonly ICryptographer _cryptographer;
    private readonly IUserRepository _userRepository;
    
    private readonly AuthenticationServiceAccessTokenConfig _accessTokenConfig;
    private readonly AuthenticationServiceRefreshTokenConfig _refreshTokenConfig;

    public UserController(
        ITokenCreator tokenCreator,
        ITokenClaimsManager tokenClaimsManager,
        ITokenLifetimeManager tokenLifetimeManager,
        ICryptographer cryptographer,
        IUserRepository userRepository,
        IOptions<AuthenticationServiceAccessTokenConfig> accessTokenConfig,
        IOptions<AuthenticationServiceRefreshTokenConfig> refreshTokenConfig)
    {
        _tokenCreator = tokenCreator;
        _tokenClaimsManager = tokenClaimsManager;
        _tokenLifetimeManager = tokenLifetimeManager;
        _cryptographer = cryptographer;
        _userRepository = userRepository;

        _accessTokenConfig = accessTokenConfig.Value;
        _refreshTokenConfig = refreshTokenConfig.Value;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegistrationModel user,
        CancellationToken cancellationToken) 
    {
        user.Password = _cryptographer.Encrypt(user.Password);
        try 
        {
            if (await _userRepository.CheckUserExistence(user.Login, cancellationToken))
                return BadRequest("Account with this login is already registered.");

            if (!await _userRepository.AddUser(user, cancellationToken))
                return Ok("Failed to add user.");
        } 
        catch 
        {
            return BadRequest("Service is temporarily unavailable.");
        }
            
        return Ok("Account was successfully registered.");
    }
    
    [HttpPost("authorize")]
    public async Task<IActionResult> Authorize(
        [FromBody] AuthenticationModel account,
        CancellationToken cancellationToken) 
    {
        string? password;
        try 
        {
            password = await _userRepository.GetUserPassword(account.Login, cancellationToken);
        } 
        catch 
        {
            return BadRequest("Service is temporarily unavailable.");
        }

        if (password is null) 
            return NotFound("User with this login does not exist");

        if (password != _cryptographer.Encrypt(account.Password))
            return BadRequest("Incorrect password");

        var claims = new List<Claim> 
        {
            new (ClaimsIdentity.DefaultNameClaimType, account.Login),
            new (ClaimsIdentity.DefaultRoleClaimType, Roles.Reader.ToString()),
        };
            
        return Ok(new 
        {
            AccessToken = _tokenCreator.CreateToken(new AccessTokenData(_accessTokenConfig), claims),
            RefreshToken = _tokenCreator.CreateToken(new RefreshTokenData(_refreshTokenConfig), claims),
        });
    }
    
    [HttpGet("refresh")]
    public IActionResult Refresh(
        CancellationToken cancellationToken) 
    {
        var refreshToken = Request.Headers["Token"].ToString();
        var refreshTokenData = new RefreshTokenData(_refreshTokenConfig);
        
        var claims = _tokenClaimsManager.GetClaims(refreshToken, refreshTokenData).ToArray();
        claims = claims[..Math.Min(claims.Length, NumberOfRequiredClaims)];
        if (_tokenLifetimeManager.GetTimeBeforeExpiration(refreshToken, refreshTokenData) < _accessTokenConfig.Lifetime)
            refreshToken = _tokenCreator.CreateToken(refreshTokenData, claims);

        
        return Ok(new 
        {
            AccessToken = _tokenCreator.CreateToken(new AccessTokenData(_accessTokenConfig), claims),
            RefreshToken = refreshToken
        });
    }
}