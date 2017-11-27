using ComponentTesting.Inprocess.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComponentTesting.Inprocess
{
    [Controller]
    public class HttpDependencyController
    {
        private readonly HttpBasedColorDisplayService _colorDisplayService;

        public HttpDependencyController(HttpBasedColorDisplayService colorDisplayService)
        {
            _colorDisplayService = colorDisplayService;
        }


        [Route("color")]
        public string GetColor()
        {
            return _colorDisplayService.GetColorData();
        }
    }
}