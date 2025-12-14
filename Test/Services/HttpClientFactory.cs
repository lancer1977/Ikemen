namespace Spotabot.Test.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private HttpClient client = new HttpClient();
        public HttpClient CreateClient(string name)
        {
            return client;
        }
    }
}