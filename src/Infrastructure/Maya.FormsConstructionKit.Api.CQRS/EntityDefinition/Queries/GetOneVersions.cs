namespace Maya.FormsConstructionKit.Api.CQRS.EntityDefinition.Queries
{
    public class GetOneVersionsQuery : IRequest<entitiesFormsResult>
    {
        public GetOneVersionsQuery(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class GetOneVersionsHandler : IRequestHandler<GetOneVersionsQuery, entitiesFormsResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<GetOneVersionsQuery> logger;

        public GetOneVersionsHandler(AppDataContext dataContext, ILogger<GetOneVersionsQuery> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<entitiesFormsResult> Handle(GetOneVersionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Library.Repositories.EntityDefinition.FindAllByNameAsync(this.dataContext, request.Name)
                    .MapAsync(storageData => Task.FromResult(
                            storageData.Select(x => x.Map())
                        ))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{nameof(GetOneVersionsHandler)}; Failled to read all versions of the {nameof(Model.Storage.EntityDefinition)} {request.Name} from the storage");
                return entitiesFormsResult.Failed(e);
            }
        }
    }
}
