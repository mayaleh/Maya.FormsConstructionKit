using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Shared.Model
{
    public static class DataSourceMapper
    {
        public static DataSourceDefinition Map(this DataSource dataSource)
        {
            return new DataSourceDefinition
            {
                CreateHttpMethod = dataSource.CreateHttpMethod.Map(),
                CreatePath = dataSource.CreatePath,
                DeleteHttpMethod = dataSource.DeleteHttpMethod.Map(),
                DeletePath = dataSource.DeletePath,
                Description = dataSource.Description,
                Endpoint = dataSource.Endpoint,
                AuthToken = dataSource.AuthToken,
                UserName = dataSource.UserName,
                Password = dataSource.Password,
                AuthKind = dataSource.AuthKind.Map(),
                QueryParams = dataSource.QueryParams,
                ReadHttpMethod = dataSource.ReadHttpMethod.Map(),
                ReadPath = dataSource.ReadPath,
                Type = dataSource.Type.Map(),
                UpdateHttpMethod = dataSource.UpdateHttpMethod.Map(),
                UpdatePath = dataSource.UpdatePath,
            };
        }

        private static Api.Model.Storage.DataSourceAuthKind Map(this Maya.FormsConstructionKit.Shared.Model.DataSourceAuthKind authKind)
        {
            return authKind switch
            {
                Maya.FormsConstructionKit.Shared.Model.DataSourceAuthKind.Basic => Api.Model.Storage.DataSourceAuthKind.Basic,
                Maya.FormsConstructionKit.Shared.Model.DataSourceAuthKind.Bearer => Api.Model.Storage.DataSourceAuthKind.Bearer,
                Maya.FormsConstructionKit.Shared.Model.DataSourceAuthKind.None => Api.Model.Storage.DataSourceAuthKind.None,
                _ => throw new NotImplementedException()
            };
        }

        private static Api.Model.Storage.DataSourceType Map(this Maya.FormsConstructionKit.Shared.Model.DataSourceType httpMethodKind)
        {
            return httpMethodKind switch
            {
                Maya.FormsConstructionKit.Shared.Model.DataSourceType.Storage => Api.Model.Storage.DataSourceType.Storage,
                Maya.FormsConstructionKit.Shared.Model.DataSourceType.Api => Api.Model.Storage.DataSourceType.Api,
                _ => throw new NotImplementedException()
            };
        }

        private static Api.Model.Storage.DataSourceHttpMethodKind Map(this Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind httpMethodKind)
        {
            return httpMethodKind switch
            {
                Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Get => Api.Model.Storage.DataSourceHttpMethodKind.Get,
                Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Post => Api.Model.Storage.DataSourceHttpMethodKind.Post,
                Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Put => Api.Model.Storage.DataSourceHttpMethodKind.Put,
                Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Delete => Api.Model.Storage.DataSourceHttpMethodKind.Delete,
                _ => throw new NotImplementedException()
            };
        }
    }
}
