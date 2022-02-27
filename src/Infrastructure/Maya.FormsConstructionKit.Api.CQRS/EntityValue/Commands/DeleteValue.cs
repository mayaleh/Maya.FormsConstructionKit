using Maya.FormsConstructionKit.Api.Library.Services.DataSource;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityValue.Commands
{
    public class DeleteValueCommand : IRequest<unitResult>
    {
        public DeleteValueCommand(string entityName, object value)
        {
            EntityName = entityName;
            Value = value;
        }

        public string EntityName { get; }
        public object Value { get; }
    }

    public class DeleteValueHandler : IRequestHandler<DeleteValueCommand, unitResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<DeleteValueHandler> logger;

        public DeleteValueHandler(AppDataContext dataContext, ILogger<DeleteValueHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<unitResult> Handle(DeleteValueCommand request, CancellationToken cancellationToken)
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

                        return await dataSource.Delete(entityDefinition, request.Value);
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return unitResult.Failed(e);
            }
        }
    }
}
