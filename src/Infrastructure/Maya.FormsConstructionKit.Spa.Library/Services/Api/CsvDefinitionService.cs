using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api;

namespace Maya.FormsConstructionKit.Spa.Library.Services.Api
{
    public class CsvDefinitionService : ICsvDefinitionService
    {
        private const string ApiPath = "api/v1/EntityCsv";

        private readonly HttpClient httpClient;

        public CsvDefinitionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Result<CsvDefinition, Exception>> AddAsync(CsvDefinition csvDefinition)
        {
            try
            {
                var response = await this.httpClient.PutAsJsonAsync(ApiPath, csvDefinition)
                    .ConfigureAwait(false);

                if (response is null)
                {
                    return Result<CsvDefinition, Exception>.Failed(new Exception("Could not add..."));
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DataResult<CsvDefinition>>();

                    if (result is null)
                        return Result<CsvDefinition, Exception>.Failed(new Exception("Unexpected response..."));

                    if (result.IsSuccess == false)
                        return Result<CsvDefinition, Exception>.Failed(new Exception(result.Message));

                    return Result<CsvDefinition, Exception>.Succeeded(result.Data!);
                }

                return Result<CsvDefinition, Exception>.Failed(new Exception($"The server responsed an unsucessfull status code: {response.StatusCode}"));
            }
            catch (Exception e)
            {
                return Result<CsvDefinition, Exception>.Failed(e);
            }
        }

        public async Task<Result<CsvDefinition[], Exception>> GetAllAsync()
        {
            try
            {
                var dataResult = await this.httpClient.GetFromJsonAsync<DataResult<CsvDefinition[]>>(ApiPath)
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<CsvDefinition[], Exception>.Failed(new Exception("Could not load data from the server"));
                }

                if (dataResult.IsSuccess)
                {
                    return Result<CsvDefinition[], Exception>.Succeeded(dataResult.Data!);
                }

                return Result<CsvDefinition[], Exception>.Failed(new Exception(dataResult.Message));
            }
            catch (Exception e)
            {
                return Result<CsvDefinition[], Exception>.Failed(e);
            }
        }

        public async Task<Result<CsvDefinition[], Exception>> GetAllForEntityAsync(string entityName)
        {
            try
            {
                var dataResult = await this.httpClient.GetFromJsonAsync<DataResult<CsvDefinition[]>>($"{ApiPath}/{entityName}")
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<CsvDefinition[], Exception>.Failed(new Exception("Could not load data from the server"));
                }

                if (dataResult.IsSuccess)
                {
                    return Result<CsvDefinition[], Exception>.Succeeded(dataResult.Data!);
                }

                return Result<CsvDefinition[], Exception>.Failed(new Exception(dataResult.Message));
            }
            catch (Exception e)
            {
                return Result<CsvDefinition[], Exception>.Failed(e);
            }
        }
    }
}
