
using Xunit;

namespace ComponentTesting.Inprocess.Tests
{
    public class HelloWorldControllerFacts
    {
        [Fact]
        public void should_pass()
        {
            Assert.True(true);
        }
        
        [Fact]
        public void should_say_hello()
        {
            var ctrl = new HelloWorldController(); 
            var indexString = ctrl.Index();
            
            Assert.Equal("Hello ASP.NET Core Application.", indexString);
        }
    }
}