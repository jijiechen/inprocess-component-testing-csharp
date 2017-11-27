using ComponentTesting.Inprocess.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComponentTesting.Inprocess
{
    [Controller]
    public class HelloWorldController
    {
        private readonly HelloWorldService _helloWorldService;

        public HelloWorldController(HelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }
        
        

        [Route("")]
        public string Index()
        {
            return _helloWorldService.SayHello();
        }
    }
}