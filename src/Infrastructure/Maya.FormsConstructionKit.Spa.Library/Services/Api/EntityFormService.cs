
using System.Net.Http.Json;
using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api;

namespace Maya.FormsConstructionKit.Spa.Library.Services.Api
{
    public class EntityFormService : IEntityFormService
    {
        private const string ApiPath = "api/v1/EntityForm";

        private readonly HttpClient httpClient;

        public EntityFormService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Result<string[], Exception>> GetAvailableAsync()
        {
            try
            {
                var dataResult = await this.httpClient.GetFromJsonAsync<DataResult<string[]>>(ApiPath + "/available")
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<string[], Exception>.Failed(new Exception("Could not load data from the server"));
                }

                if (dataResult.IsSuccess)
                {
                    return Result<string[], Exception>.Succeeded(dataResult.Data!);
                }

                return Result<string[], Exception>.Failed(new Exception(dataResult.Message));
            }
            catch (Exception e)
            {
                return Result<string[], Exception>.Failed(e);
            }
        }
        
        public async Task<Result<Shared.Model.EntityForm[], Exception>> GetAllAsync()
        {
            try
            {
                var dataResult = await this.httpClient.GetFromJsonAsync<DataResult<Shared.Model.EntityForm[]>>(ApiPath)
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<Shared.Model.EntityForm[], Exception>.Failed(new Exception("Could not load data from the server"));
                }

                if (dataResult.IsSuccess)
                {
                    return Result<Shared.Model.EntityForm[], Exception>.Succeeded(dataResult.Data!);
                }

                return Result<Shared.Model.EntityForm[], Exception>.Failed(new Exception(dataResult.Message));
            }
            catch (Exception e)
            {
                return Result<Shared.Model.EntityForm[], Exception>.Failed(e);
            }
        }

        public async Task<Result<Shared.Model.EntityForm, Exception>> AddAsync(Shared.Model.EntityForm entityForm)
        {
            try
            {
                var response = await this.httpClient.PutAsJsonAsync(ApiPath, entityForm)
                    .ConfigureAwait(false);

                if (response is null)
                {
                    return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception("Could not add..."));
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DataResult<Shared.Model.EntityForm>>();

                    if (result is null)
                        return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception("Unexpected response..."));

                    if (result.IsSuccess == false)
                        return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception(result.Message));

                    return Result<Shared.Model.EntityForm, Exception>.Succeeded(result.Data!);
                }

                return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception($"The server responsed an unsucessfull status code: {response.StatusCode}"));
            }
            catch (Exception e)
            {
                return Result<Shared.Model.EntityForm, Exception>.Failed(e);
            }
        }

        public async Task<Result<EntityForm, Exception>> UdpateAsync(EntityForm entityForm)
        {
            try
            {
                var response = await this.httpClient.PostAsJsonAsync(ApiPath, entityForm)
                    .ConfigureAwait(false);

                if (response is null)
                {
                    return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception("Could not update..."));
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DataResult<Shared.Model.EntityForm>>();

                    if (result is null)
                        return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception("Unexpected response..."));

                    if (result.IsSuccess == false)
                        return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception(result.Message));

                    return Result<Shared.Model.EntityForm, Exception>.Succeeded(result.Data!);
                }

                return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception($"The server responsed an unsucessfull status code: {response.StatusCode}"));
            }
            catch (Exception e)
            {
                return Result<Shared.Model.EntityForm, Exception>.Failed(e);
            }
        }

        public async Task<Result<Maya.Ext.Unit, Exception>> DeleteAsync(string name)
        {
            try
            {
                var response = await this.httpClient.DeleteAsync($"{ApiPath}/{name}")
                    .ConfigureAwait(false);

                if (response is null)
                {
                    return Result<Ext.Unit, Exception>.Failed(new Exception("Could not add..."));
                }

                if (response.IsSuccessStatusCode)
                {
                    return Result<Ext.Unit, Exception>.Succeeded(Ext.Unit.Default);
                }

                return Result<Ext.Unit, Exception>.Failed(new Exception($"The server responsed an unsucessfull status code: {response.StatusCode}"));
            }
            catch (Exception e)
            {
                return Result<Ext.Unit, Exception>.Failed(e);
            }
        }

        public async Task<Result<EntityForm, Exception>> GetLatestOneAsync(string name)
        {
            try
            {
                var dataResult = await this.httpClient.GetFromJsonAsync<DataResult<Shared.Model.EntityForm>>(ApiPath + "/latest/" + name)
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception("Could not load data from the server"));
                }

                if (dataResult.IsSuccess)
                {
                    return Result<Shared.Model.EntityForm, Exception>.Succeeded(dataResult.Data!);
                }

                return Result<Shared.Model.EntityForm, Exception>.Failed(new Exception(dataResult.Message));
            }
            catch (Exception e)
            {
                return Result<Shared.Model.EntityForm, Exception>.Failed(e);
            }
        }

        public async Task<Result<EntityForm[], Exception>> GetOneVersionsAsync(string name)
        {
            try
            {
                var dataResult = await this.httpClient.GetFromJsonAsync<DataResult<Shared.Model.EntityForm[]>>(ApiPath + "/" + name)
                    .ConfigureAwait(false);

                if (dataResult is null)
                {
                    return Result<Shared.Model.EntityForm[], Exception>.Failed(new Exception("Could not load data from the server"));
                }

                if (dataResult.IsSuccess)
                {
                    return Result<Shared.Model.EntityForm[], Exception>.Succeeded(dataResult.Data!);
                }

                return Result<Shared.Model.EntityForm[], Exception>.Failed(new Exception(dataResult.Message));
            }
            catch (Exception e)
            {
                return Result<Shared.Model.EntityForm[], Exception>.Failed(e);
            }
        }
    }
}
