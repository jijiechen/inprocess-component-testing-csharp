using System;
using ComponentTesting.Inprocess.App;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentTesting.Inprocess.Tests
{
    public class FactBase
    {
        protected static ServiceProvider ConfigureAppServices(Action<ServiceCollection> configure = null)
        {
            var services = new ServiceCollection();
            ApplicationContext.ConfigureServices(services);
            configure?.Invoke(services);

            var scope = services.BuildServiceProvider();
            return scope;
        }
    }
}