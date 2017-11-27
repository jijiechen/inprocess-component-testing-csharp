using ComponentTesting.Inprocess.Models;
using Newtonsoft.Json;

namespace ComponentTesting.Inprocess.Services
{
    public class HttpBasedColorDisplayService
    {
        private readonly IHttpInvoker _httpInvoker;

        public HttpBasedColorDisplayService(IHttpInvoker httpInvoker)
        {
            _httpInvoker = httpInvoker;
        }

        public virtual string GetColorData()
        {
            var colorResource = _httpInvoker.InvokeHttp("http://adobe.github.io/Spry/data/json/object-01.js");
            var colorData = JsonConvert.DeserializeObject<ColorData>(colorResource);
            
            var valueString = $"#{colorData.value}";
            return $"{colorData.color}:{valueString}";
        }
    }
}