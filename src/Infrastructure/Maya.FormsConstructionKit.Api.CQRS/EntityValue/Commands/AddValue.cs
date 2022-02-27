using Maya.FormsConstructionKit.Api.Library.Services.DataSource;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityValue.Commands
{
    public class AddValueCommand : IRequest<entityValueResult>
    {
        public AddValueCommand(string entityName, object value)
        {
            EntityName = entityName;
            Value = value;
        }

        public string EntityName { get; }
        public object Value { get; }
    }

    public class AddValueHandler : IRequestHandler<AddValueCommand, entityValueResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<AddValueHandler> logger;

        public AddValueHandler(AppDataContext dataContext, ILogger<AddValueHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<entityValueResult> Handle(AddValueCommand request, CancellationToken cancellationToken)
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

                        return await dataSource.Create(entityDefinition, request.Value);
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{nameof(AddValueHandler)}; Failled to create new value.");
                return entityValueResult.Failed(e);
            }
        }
    }
}
