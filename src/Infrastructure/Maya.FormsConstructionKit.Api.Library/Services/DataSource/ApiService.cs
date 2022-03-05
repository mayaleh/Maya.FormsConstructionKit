using Maya.AnyHttpClient.Model;
using Maya.Ext;
using Maya.Ext.Rop;

namespace Maya.FormsConstructionKit.Api.Library.Services.DataSource
{
    internal class ApiService : Maya.AnyHttpClient.ApiService
    {
        public ApiService(IHttpClientConnector httpClientConnenctor) : base(httpClientConnenctor)
        {
        }

        public async Task<Result<T, Exception>> Get<T>(UriRequest uriRequest)
        {
            return await this.HttpGet<T>(uriRequest);
        }
        public async Task<Result<T, Exception>> Post<T>(UriRequest uriRequest, object? data = null)
        {
            return await this.HttpPost<T>(uriRequest, data ?? new { });
        }
    }
}
