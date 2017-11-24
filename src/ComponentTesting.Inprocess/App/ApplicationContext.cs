using System;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentTesting.Inprocess.App
{
    public class ApplicationContext
    {
        public static ServiceProvider BuildServiceProvider(Action<ServiceCollection> setupServiceProvider = null)
        {
            var services = new ServiceCollection();
            
            services.AddSingleton<HelloWorldService>();
            services.AddTransient<HelloWorldController>();
            setupServiceProvider?.Invoke(services);

            return services.BuildServiceProvider();
        }
        
    }
}