using ComponentTesting.Inprocess.App;
using ComponentTesting.Inprocess.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ComponentTesting.Inprocess.Tests.InitegrationTests
{
    public class HelloWorldControllerIntFacts
    {
        
        [Fact]
        public async void should_handle_web_request()
        {
            var server = new TestServer(WebApplication.CreateWebHost());
            var client = server.CreateClient();

            var response = await client.GetAsync("/");
            var indexString = await response.Content.ReadAsStringAsync();
            
            Assert.Equal("Hello ASP.NET Core Application.", indexString);
        }
        
        
        [Fact]
        public async void should_handle_web_request_with_mocked_service()
        {
            void ConfigServices(IServiceCollection services)
            {
                var helloService = new Mock<HelloWorldService>();
                helloService.Setup(s => s.SayHello()).Returns("Hello from mocked service.");
                services.AddSingleton(helloService.Object);
            }

            var server = new TestServer(WebApplication.CreateWebHost(ConfigServices));
            var client = server.CreateClient();
            
            var response = await client.GetAsync("/");
            var indexString = await response.Content.ReadAsStringAsync();
            
            Assert.Equal("Hello from mocked service.", indexString);
        }
    }
}