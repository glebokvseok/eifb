using DependencyRegistrar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using YandexCloud.Clients;

namespace YandexCloud;

public class Module : IModule
{
    public void Register(HostBuilderContext context, IServiceCollection services)
    {
        services
            .AddOptions<YandexCloudStorageClientConfig>()
            .Bind(context.Configuration.GetSection(nameof(YandexCloudStorageClientConfig)));
        services.TryAddSingleton<IYandexCloudStorageClient, YandexCloudStorageClient>();
    }
}