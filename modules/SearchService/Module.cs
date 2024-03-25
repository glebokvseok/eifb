using DependencyRegistrar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchService.Services;

namespace SearchService;

public class Module : IModule
{
    public void Register(HostBuilderContext context, IServiceCollection services)
    {
        services
            .AddOptions<SearchRepositoryConfig>()
            .Bind(context.Configuration.GetSection(nameof(SearchRepositoryConfig)));
        services.AddSingleton<ISearchRepository, SearchRepository>();
        services.AddSingleton<ISearchRequestBuilder, SearchRequestBuilder>();
    }
}