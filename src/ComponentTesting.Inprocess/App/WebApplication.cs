using System;
using ComponentTesting.Inprocess.Data;
using ComponentTesting.Inprocess.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ComponentTesting.Inprocess.App
{
    public static class WebApplication
    {
        internal static void ConfigigureWebHost()
        {
            var host = CreateWebHost()
                .UseKestrel()
                .Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHost(
            Action<IServiceCollection> onConfiguringServices = null, 
            Action<IServiceCollection> onConfiguredServices = null,
            Action<IApplicationBuilder> onConfiguringApp = null)
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    onConfiguringServices?.Invoke(services);
                    
                    ConfigureAppServices(services);
                    
                    onConfiguredServices?.Invoke(services);
                })
                .Configure(app =>
                {
                    app.UseMvc();

                    onConfiguringApp?.Invoke(app);
                });
            return hostBuilder;
        }

        public static void ConfigureAppServices(IServiceCollection services)
        {
            services.AddMvcCore();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql("server=localhost;database=employees;uid=root;pwd=admin");
            });
            services.AddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));

            services.AddSingleton<HelloWorldService>();
            services.AddTransient<HelloWorldController>();
            
            services.AddSingleton<IHttpInvoker, DefaultHttpInvoker>();
            services.AddSingleton<HttpBasedColorDisplayService>();
            services.AddTransient<HttpDependencyController>();
            
            services.AddScoped<EmployeeService>();
            services.AddScoped<EmployeeController>();
        }
    }
}