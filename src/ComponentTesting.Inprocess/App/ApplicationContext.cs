using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace ComponentTesting.Inprocess.App
{
    public static class ApplicationContext
    {
        public static void ConfigigureWebHost()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(ConfigureServices)
                .Configure(app =>
                {
                    app.UseMvc();
                })
                .Build();

            host.Run();
        }


        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
            services.AddSingleton<HelloWorldService>();
            services.AddTransient<HelloWorldController>();
        }
    }
}