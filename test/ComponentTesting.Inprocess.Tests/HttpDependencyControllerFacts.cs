
using ComponentTesting.Inprocess.Models;
using ComponentTesting.Inprocess.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ComponentTesting.Inprocess.Tests
{
    public class HttpDependencyControllerFacts: FactBase
    {
        [Fact]
        public void should_be_able_to_serve_mocked_service()
        {
            var mockedColorService = new Mock<HttpBasedColorDisplayService>(null);
            mockedColorService.Setup(s => s.GetColorData()).Returns("mocked:#ab9900");
            var scope = ConfigureAppServices(services => services.AddSingleton(mockedColorService.Object));

            var ctrl = scope.GetService<HttpDependencyController>();
            var colorString = ctrl.GetColor();
            
            Assert.Equal("mocked:#ab9900", colorString);
        }
        
        [Fact]
        public void should_be_able_to_serve_mocked_http_dependency()
        {
            var mockedHttpInvoker = new Mock<IHttpInvoker>();
            mockedHttpInvoker.Setup(s => s.InvokeHttp(It.IsAny<string>())).Returns("{\"color\":\"olive\",\"value\":\"ab9900\"}");
            var scope = ConfigureAppServices(services => services.AddSingleton(typeof(IHttpInvoker), mockedHttpInvoker.Object));

            var ctrl = scope.GetService<HttpDependencyController>();
            var colorString = ctrl.GetColor();
            
            Assert.Equal("olive:#ab9900", colorString);
        }

    }

}