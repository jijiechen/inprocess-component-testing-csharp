namespace ComponentTesting.Inprocess
{
    public class HelloWorldController
    {
        private readonly HelloWorldService _helloWorldService;

        public HelloWorldController(HelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }
        
        

        public string Index()
        {
            return _helloWorldService.SayHello();
        }
    }
}