using DependencyRegistrar;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace eifb;

public static class Program {
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    private static IHostBuilder CreateHostBuilder(string[] args) 
    {
        return Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(Registrar.Register<ArticleManager.Module>)
            .ConfigureServices(Registrar.Register<AuthenticationService.Module>)
            .ConfigureServices(Registrar.Register<SearchService.Module>)
            .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}