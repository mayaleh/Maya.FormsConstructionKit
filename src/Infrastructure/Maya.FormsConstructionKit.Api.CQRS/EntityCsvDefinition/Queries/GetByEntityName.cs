using AutoMapper;
using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityCsvDefinition.Queries
{
    public class GetByEntityNameQuery : IRequest<csvDefinitionsResult>
    {
        public GetByEntityNameQuery(string entityName)
        {
            EntityName = entityName;
        }

        public string EntityName { get; }
    }

    public class GetByEntityNameHanler : IRequestHandler<GetByEntityNameQuery, csvDefinitionsResult>
    {
        private readonly IMapper mapper;
        private readonly AppDataContext dataContext;
        private readonly ILogger<GetByEntityNameHanler> logger;

        public GetByEntityNameHanler(IMapper mapper, AppDataContext dataContext, ILogger<GetByEntityNameHanler> logger)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<csvDefinitionsResult> Handle(GetByEntityNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Library.Repositories.EntityCsvDefinition.AllForEntityAsync(this.dataContext, request.EntityName)
                    .MapAsync(data => Task.FromResult(
                            data.Select(x => this.mapper.Map<Shared.Model.CsvDefinition>(x))
                        ))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{nameof(GetByEntityNameHanler)}");
                return csvDefinitionsResult.Failed(e);
            }
        }
    }
}
