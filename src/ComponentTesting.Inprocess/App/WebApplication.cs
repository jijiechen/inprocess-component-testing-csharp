using System;
using ComponentTesting.Inprocess.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public static IWebHostBuilder CreateWebHost(Action<IServiceCollection> serviceConfiguration = null)
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    ConfigureAppServices(services);
                    serviceConfiguration?.Invoke(services);
                })
                .Configure(app => { app.UseMvc(); });
            return hostBuilder;
        }

        public static void ConfigureAppServices(IServiceCollection services)
        {
            services.AddMvcCore();
            services.AddSingleton<HelloWorldService>();
            services.AddTransient<HelloWorldController>();
            
            services.AddSingleton<IHttpInvoker, DefaultHttpInvoker>();
            services.AddSingleton<HttpBasedColorDisplayService>();
            services.AddTransient<HttpDependencyController>();
        }
    }
}