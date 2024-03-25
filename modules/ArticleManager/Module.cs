using ArticleManager.Services;
using DependencyRegistrar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArticleManager;

public class Module : IModule
{

    public void Register(HostBuilderContext context, IServiceCollection services)
    {
        services
            .AddOptions<ArticleRepositoryConfig>()
            .Bind(context.Configuration.GetSection(nameof(ArticleRepositoryConfig)));
        services.AddSingleton<IArticleRepository, ArticleRepository>();
        Registrar.Register<YandexCloud.Module>(context, services);
    }
}