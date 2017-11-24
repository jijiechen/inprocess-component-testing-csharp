
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ComponentTesting.Inprocess.Tests
{
    public class HelloWorldControllerFacts: FactBase
    {
        [Fact]
        public void should_pass()
        {
            Assert.True(true);
        }
        
        [Fact]
        public void should_say_hello()
        {
            var scope = ConfigureAppServices();
            
            var ctrl = scope.GetService<HelloWorldController>();
            var indexString = ctrl.Index();
            
            Assert.Equal("Hello ASP.NET Core Application.", indexString);
        }
        
        [Fact]
        public void should_be_able_to_mock_hello_service()
        {
            var helloService = new Mock<HelloWorldService>();
            helloService.Setup(s => s.SayHello()).Returns("Hello from mocked service.");
            var scope = ConfigureAppServices(services => services.AddSingleton(helloService.Object));

            var ctrl = scope.GetService<HelloWorldController>();
            var indexString = ctrl.Index();
            
            Assert.Equal("Hello from mocked service.", indexString);
        }
    }
}