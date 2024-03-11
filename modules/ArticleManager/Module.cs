using DependencyRegistrar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ArticleManager;

public class Module : IModule
{

    public void Register(HostBuilderContext context, IServiceCollection services)
    {
        Registrar.Register<YandexCloud.Module>(context, services);
        services.AddSingleton<IStorageKeeper>(new StorageKeeper());
    }
}