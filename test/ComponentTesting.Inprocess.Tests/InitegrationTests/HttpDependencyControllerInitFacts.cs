using ComponentTesting.Inprocess.App;
using ComponentTesting.Inprocess.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ComponentTesting.Inprocess.Tests.InitegrationTests
{
    public class HttpDependencyControllerInitFacts
    {
        [Fact(Skip = "This case replies on real network integration")]
        public async void should_handle_web_request()
        {
            var server = new TestServer(WebApplication.CreateWebHost());
            var client = server.CreateClient();

            var response = await client.GetAsync("/color");
            var indexString = await response.Content.ReadAsStringAsync();
            
            Assert.Equal("red:#f00", indexString);
        }
        
        [Fact]
        public async void should_handle_web_request_with_mocked_component()
        {
            var mockedHttpInvoker = new Mock<IHttpInvoker>();
            mockedHttpInvoker.Setup(s => s.InvokeHttp(It.IsAny<string>())).Returns("{\"color\":\"olive\",\"value\":\"#ab9900\"}");

            var server = new TestServer(WebApplication.CreateWebHost(null, services => services.AddSingleton(typeof(IHttpInvoker), mockedHttpInvoker.Object)));
            var client = server.CreateClient();

            var response = await client.GetAsync("/color");
            var indexString = await response.Content.ReadAsStringAsync();
            
            Assert.Equal("olive:#ab9900", indexString);
        }
    }
}