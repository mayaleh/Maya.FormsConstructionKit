using System.Net.Http.Json;
using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api;

namespace Maya.FormsConstructionKit.Spa.Library.Services.Api
{
    public class ValuesService : IValuesService
    {
        private const string ApiPath = "api/v1/Values";

        private readonly HttpClient httpClient;

        public ValuesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Result<EntityData, Exception>> GetAllAsync(string entityName)
        {
            try
            {
                if (string.IsNullOrEmpty(entityName))
                {
                    return Result<EntityData, Exception>.Failed(new ArgumentException("The Form / Entity name is required!"));
                }

                var dataResult = await this.httpClient.GetFromJsonAsync<DataResult<EntityData>>($"{ApiPath}/{entityName}")
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<EntityData, Exception>.Failed(new Exception("Could not load data from the server"));
                }

                if (dataResult.IsSuccess)
                {
                    return Result<EntityData, Exception>.Succeeded(dataResult.Data!);
                }

                return Result<EntityData, Exception>.Failed(new Exception(dataResult.Message));
            }
            catch (Exception e)
            {
                return Result<EntityData, Exception>.Failed(e);
            }
        }

        public async Task<Result<DownloadFile, Exception>> GetCsvAsync(string entityName, string csvId)
        {
            try
            {
                if (string.IsNullOrEmpty(entityName))
                {
                    return Result<DownloadFile, Exception>.Failed(new ArgumentException("The Form / Entity name is required!"));
                }

                var response = await this.httpClient.GetAsync($"{ApiPath}/csv/{entityName}/{csvId}")
                    .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var ms = new MemoryStream((int)stream.Length);
                    stream.CopyTo(ms);
                    stream.Close();
                    var fileName = response.Content.Headers.ContentDisposition?.FileName?.TrimStart('"')
                        ?.TrimEnd('"');

                    var fileModel = new DownloadFile
                    {
                        Content = ms.ToArray(),
                        ContentType = response.Content.Headers.ContentType?.ToString(),
                        Name = fileName ?? "file"
                    };

                    return Result<DownloadFile, Exception>.Succeeded(fileModel);
                }

                return Result<DownloadFile, Exception>.Failed(new Exception($"Failled to download the file: {response.StatusCode}"));
            }
            catch (Exception e)
            {
                return Result<DownloadFile, Exception>.Failed(e);
            }
        }

        public async Task<Result<object, Exception>> AddAsync(string entityName, object value)
        {
            try
            {
                var response = await this.httpClient.PutAsJsonAsync($"{ApiPath}/{entityName}", value)
                    .ConfigureAwait(false);

                if (response is null)
                {
                    return Result<object, Exception>.Failed(new Exception("Could not add..."));
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DataResult<object>>();

                    if (result is null)
                        return Result<object, Exception>.Failed(new Exception("Unexpected response..."));

                    if (result.IsSuccess == false)
                        return Result<object, Exception>.Failed(new Exception(result.Message));

                    return Result<object, Exception>.Succeeded(result.Data!);
                }

                return Result<object, Exception>.Failed(new Exception($"The server responsed an unsucessfull status code: {response.StatusCode}"));
            }
            catch (Exception e)
            {
                return Result<object, Exception>.Failed(e);
            }
        }

        public async Task<Result<object, Exception>> UpdateAsync(string entityName, object value)
        {
            try
            {
                var response = await this.httpClient.PostAsJsonAsync($"{ApiPath}/{entityName}", value)
                    .ConfigureAwait(false);

                if (response is null)
                {
                    return Result<object, Exception>.Failed(new Exception("Could not update..."));
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DataResult<object>>();

                    if (result is null)
                        return Result<object, Exception>.Failed(new Exception("Unexpected response..."));

                    if (result.IsSuccess == false)
                        return Result<object, Exception>.Failed(new Exception(result.Message));

                    return Result<object, Exception>.Succeeded(result.Data!);
                }

                return Result<object, Exception>.Failed(new Exception($"The server responsed an unsucessfull status code: {response.StatusCode}"));
            }
            catch (Exception e)
            {
                return Result<object, Exception>.Failed(e);
            }
        }
    }
}
