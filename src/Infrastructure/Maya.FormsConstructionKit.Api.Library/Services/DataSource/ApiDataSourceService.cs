using Maya.Ext;
using Maya.Ext.Rop;
using Maya.FormsConstructionKit.Api.Library.Mappers.Storage;

namespace Maya.FormsConstructionKit.Api.Library.Services.DataSource
{
    public class ApiDataSourceService : IDataSourceService
    {
        public Task<Result<object, Exception>> Create(EntityDefinition entityDefinition, object entityValue)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Unit, Exception>> Delete(EntityDefinition entityDefinition, object entityValue)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<object>, Exception>> Read(EntityDefinition entityDefinition)
        {
            try
            {
                return await CreateApiService(entityDefinition.DataSourceDefinition)
                    .BindAsync(async apiService =>
                    {
                        var uri = CreateUriRequest(entityDefinition.DataSourceDefinition.ReadPath);

                        var requestTask = entityDefinition.DataSourceDefinition.ReadHttpMethod switch
                        {
                            DataSourceHttpMethodKind.Get => apiService.Get<IEnumerable<object>>(uri),
                            DataSourceHttpMethodKind.Post => apiService.Post<IEnumerable<object>>(uri),
                            _ => throw new NotImplementedException("For reading data is supported only HTTP methods GET and POST")
                        };

                        return await requestTask;
                    });
            }
            catch (Exception e)
            {
                return Result<IEnumerable<object>, Exception>.Failed(e);
            }
        }

        public Task<Result<object, Exception>> Update(EntityDefinition entityDefinition, object entityValue)
        {
            throw new NotImplementedException();
        }

        public IDataSourceService WithLogger(ILogger logger)
        {
            throw new NotImplementedException();
        }

        private static Maya.AnyHttpClient.Model.UriRequest CreateUriRequest(string path, List<PropertyValue> values = null)
        {
            if (values != null)
            {
                path = ReplaceVariablesWithValue(path, values);
            }

            return new Maya.AnyHttpClient.Model.UriRequest(new List<string> { path });
        }

        private static string ReplaceVariablesWithValue(string originalText, List<PropertyValue> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                var current = values[i];
                var possibleName = new StringBuilder("{{");
                possibleName.Append(current.Name);
                possibleName.Append("}}");
                var nameOfVar = possibleName.ToString();
                var valueOfVar = current.Type switch
                {
                    ContentType.Bool => current.BoolVal.ToString(),
                    ContentType.DateOnly => current.DateVal.ToString(),
                    ContentType.DateTime => current.DateTimeVal.ToString(),
                    ContentType.Decimal => current.DecimalVal.ToString(),
                    ContentType.Float => current.FloatVal.ToString(),
                    ContentType.Int => current.IntVal.ToString(),
                    ContentType.Long => current.LongVal.ToString(),
                    ContentType.Text => current.TextVal,
                    _ => throw new NotImplementedException(current.Type.ToString())
                };

                originalText = originalText.Replace(nameOfVar, valueOfVar);
            }

            return Uri.EscapeDataString(originalText);
        }

        private static Task<Result<ApiService, Exception>> CreateApiService(DataSourceDefinition dataSourceDefinition)
        {
            try
            {
                if (string.IsNullOrEmpty(dataSourceDefinition.Endpoint))
                {
                    return Task.FromResult(Result<ApiService, Exception>.Failed(new Exception("The endpoind is required!")));
                }

                var httpConfig = new Maya.AnyHttpClient.Model.HttpClientConnector
                {
                    AuthType = dataSourceDefinition.AuthKind switch
                    {
                        DataSourceAuthKind.None => string.Empty,
                        DataSourceAuthKind.Basic => Maya.AnyHttpClient.Model.AuthTypeKinds.Basic,
                        DataSourceAuthKind.Bearer => Maya.AnyHttpClient.Model.AuthTypeKinds.Bearer,
                        _ => throw new NotImplementedException(dataSourceDefinition.AuthKind.ToString())
                    },
                    Token = dataSourceDefinition.AuthToken,
                    UserName = dataSourceDefinition.UserName,
                    Password = dataSourceDefinition.Password,
                    Headers = new List<Maya.AnyHttpClient.Model.KeyValue>
                    {
                        new AnyHttpClient.Model.KeyValue { Name = "X-App-Name", Value = "Maya.FormsConstructionKit"}
                    },
                    Endpoint = dataSourceDefinition.Endpoint,
                    TimeoutSeconds = dataSourceDefinition.TimeoutSeconds == default(int) ? 30 : dataSourceDefinition.TimeoutSeconds,
                };

                var apiService = new ApiService(httpConfig);
                return Task.FromResult(Result<ApiService, Exception>.Succeeded(apiService));
            }
            catch (Exception e)
            {
                return Task.FromResult(Result<ApiService, Exception>.Failed(e));
            }
        }
    }
}
