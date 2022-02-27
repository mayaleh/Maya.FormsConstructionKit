using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api;
using Maya.FormsConstructionKit.Spa.Library.Services.Api;

namespace Maya.FormsConstructionKit.Spa.Library.Services
{
    public class ApiService : IApiService
    {
        public IEntityFormService EntityForm { get; private set; }
        public IValuesService ValuesService { get; private set; }
        public ICsvDefinitionService CsvDefinitionService { get; }

        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            EntityForm = new EntityFormService(httpClient);
            ValuesService = new ValuesService(httpClient);
            CsvDefinitionService = new CsvDefinitionService(this.httpClient);
        }

        public ApiService(IHttpClientFactory clientFactory)
        {
            this.httpClient = clientFactory.CreateClient(nameof(Maya.FormsConstructionKit.Spa.Library.Contract.Services.IApiService));
            EntityForm = new EntityFormService(this.httpClient);
            ValuesService = new ValuesService(this.httpClient);
            CsvDefinitionService = new CsvDefinitionService(this.httpClient);
        }

        public async Task<Result<Shared.Model.AppInfo, Exception>> GetApiInfoAsync()
        {
            try
            {
                var dataResult = await this.httpClient.GetFromJsonAsync<Shared.Model.AppInfo>("api/Info")
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<Shared.Model.AppInfo, Exception>.Failed(new Exception("Could not load data from the server"));
                }

                return Result<Shared.Model.AppInfo, Exception>.Succeeded(dataResult);
            }
            catch (Exception e)
            {
                return Result<Shared.Model.AppInfo, Exception>.Failed(e);
            }
        }

    }
}
