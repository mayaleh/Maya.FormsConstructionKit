using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityDefinition.Queries
{
    public class GetOneLatestQuery : IRequest<entityFormResult>
    {
        public GetOneLatestQuery(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class GetOneLatestHandler : IRequestHandler<GetOneLatestQuery, entityFormResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<GetOneLatestHandler> logger;

        public GetOneLatestHandler(AppDataContext dataContext, ILogger<GetOneLatestHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<Result<EntityForm, Exception>> Handle(GetOneLatestQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Library.Repositories.EntityDefinition.FindLatestByNameAsync(this.dataContext, request.Name)
                    .MapAsync(x =>
                        Task.FromResult(x.Map())
                     )
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{nameof(GetOneLatestHandler)}; Failled to read lates {nameof(Model.Storage.EntityDefinition)} {request.Name} from the storage");
                return entityFormResult.Failed(e);
            }
        }
    }
}
