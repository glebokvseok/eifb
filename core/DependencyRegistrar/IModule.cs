using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DependencyRegistrar;

public interface IModule
{
    void Register(HostBuilderContext context, IServiceCollection services);
}