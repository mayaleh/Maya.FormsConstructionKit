using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Storage
{
    public static class DataSourceDefinitionMapper
    {
        public static DataSource Map(this DataSourceDefinition dataSourceDefinition)
        {
            return new DataSource
            {
                CreateHttpMethod = dataSourceDefinition.CreateHttpMethod.Map(),
                CreatePath = dataSourceDefinition.CreatePath,
                DeleteHttpMethod = dataSourceDefinition.DeleteHttpMethod.Map(),
                DeletePath = dataSourceDefinition.DeletePath,
                Description = dataSourceDefinition.Description,
                Endpoint = dataSourceDefinition.Endpoint,
                QueryParams = dataSourceDefinition.QueryParams,
                ReadHttpMethod = dataSourceDefinition.ReadHttpMethod.Map(),
                ReadPath = dataSourceDefinition.ReadPath,
                Type = dataSourceDefinition.Type.Map(),
                UpdateHttpMethod = dataSourceDefinition.UpdateHttpMethod.Map(),
                UpdatePath = dataSourceDefinition.UpdatePath,
            };
        }

        private static Maya.FormsConstructionKit.Shared.Model.DataSourceType Map(this Api.Model.Storage.DataSourceType httpMethodKind)
        {
            return httpMethodKind switch
            {
                Api.Model.Storage.DataSourceType.Storage => Maya.FormsConstructionKit.Shared.Model.DataSourceType.Storage,
                Api.Model.Storage.DataSourceType.Api => Maya.FormsConstructionKit.Shared.Model.DataSourceType.Api,
                _ => throw new NotImplementedException()
            };
        }

        private static Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind Map(this Api.Model.Storage.DataSourceHttpMethodKind httpMethodKind)
        {
            return httpMethodKind switch
            {
                Api.Model.Storage.DataSourceHttpMethodKind.Get => Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Get,
                Api.Model.Storage.DataSourceHttpMethodKind.Post => Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Post,
                Api.Model.Storage.DataSourceHttpMethodKind.Put => Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Put,
                Api.Model.Storage.DataSourceHttpMethodKind.Delete => Maya.FormsConstructionKit.Shared.Model.DataSourceHttpMethodKind.Delete,
                _ => throw new NotImplementedException()
            };
        }
    }
}
