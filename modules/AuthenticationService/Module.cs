using AuthenticationService.Models.Tokens;
using AuthenticationService.Services;
using DependencyRegistrar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace AuthenticationService;

public class Module : IModule
{
    public void Register(HostBuilderContext context, IServiceCollection services)
    {
        services
            .AddOptions<AuthenticationServiceAccessTokenConfig>()
            .Bind(context.Configuration.GetSection(nameof(AuthenticationServiceAccessTokenConfig)));
        services
            .AddOptions<AuthenticationServiceRefreshTokenConfig>()
            .Bind(context.Configuration.GetSection(nameof(AuthenticationServiceRefreshTokenConfig)));
        services
            .AddOptions<UserRepositoryConfig>()
            .Bind(context.Configuration.GetSection(nameof(UserRepositoryConfig)));
        services.TryAddSingleton<ICryptographer, Cryptographer>();
        services.TryAddSingleton<ITokenCreator, TokenCreator>();
        services.TryAddSingleton<ITokenClaimsManager, TokenDataManager>();
        services.TryAddSingleton<ITokenLifetimeManager, TokenDataManager>();
        services.TryAddSingleton<IUserRepository, UserRepository>();
    }
}