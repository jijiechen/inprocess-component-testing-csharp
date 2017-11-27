namespace ComponentTesting.Inprocess.Services
{
    public interface IHttpInvoker
    {
        string InvokeHttp(string url);
    }
}