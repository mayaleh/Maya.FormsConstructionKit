using Maya.FormsConstructionKit.Api.Library.Services.DataSource;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityValue.Commands
{
    public class UpdateValueCommand : IRequest<entityValueResult>
    {
        public UpdateValueCommand(string entityName, object data)
        {
            EntityName = entityName;
            Data = data;
        }

        public string EntityName { get; }
        public object Data { get; }
    }

    public class UpdateValueHandler : IRequestHandler<UpdateValueCommand, entityValueResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<UpdateValueHandler> logger;

        public UpdateValueHandler(AppDataContext dataContext, ILogger<UpdateValueHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<entityValueResult> Handle(UpdateValueCommand request, CancellationToken cancellationToken)
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

                        return await dataSource.Update(entityDefinition, request.Data);
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return entityValueResult.Failed(e);
            }
        }
    }
}
