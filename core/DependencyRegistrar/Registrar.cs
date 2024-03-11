using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DependencyRegistrar;

public static class Registrar
{
    public static void Register<TModule>(HostBuilderContext context, IServiceCollection services)
        where TModule : IModule, new() => new TModule().Register(context, services);
}