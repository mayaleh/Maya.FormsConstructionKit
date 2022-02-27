using Maya.FormsConstructionKit.Api.Library.Services.DataSource;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityValue.Queries
{
    public class GetValuesQuery : IRequest<entityValuesResult>
    {
        public GetValuesQuery(string entityName)
        {
            EntityName = entityName;
        }

        public string EntityName { get; }
    }

    public class GetValuesHandler : IRequestHandler<GetValuesQuery, entityValuesResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<GetValuesHandler> logger;

        public GetValuesHandler(AppDataContext dataContext, ILogger<GetValuesHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<entityValuesResult> Handle(GetValuesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Library.Repositories.EntityDefinition.FindLatestByNameAsync(this.dataContext, request.EntityName)
                    .BindAsync(async entityDefinition => {
                        IDataSourceService dataSource = entityDefinition.DataSourceDefinition.Type switch
                        {
                            DataSourceType.Api => throw new NotImplementedException(),
                            DataSourceType.Storage => new StorageDataSourceService(this.dataContext)
                                .WithLogger(this.logger),
                            _ => throw new NotImplementedException(),
                        };

                        return await dataSource.Read(entityDefinition)
                            .MapAsync(data => {
                                var result = new Shared.Model.EntityData
                                {
                                    EntityForm = entityDefinition.Map(),
                                    Data = data
                                };

                                // avoid returning datasource settings
                                result.EntityForm.DataSource = new();
                                
                                return Task.FromResult(result);
                            });
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return entityValuesResult.Failed(e);
            }
        }
    }
}
