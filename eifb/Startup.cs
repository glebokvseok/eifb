using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eifb; 

public class Startup {
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
    {
        if (env.IsDevelopment()) 
        {
            app.UseDeveloperExceptionPage();
        }
            
        app.UseCors(builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();
            
        app.UseAuthentication();
        app.UseAuthorization();
            
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}