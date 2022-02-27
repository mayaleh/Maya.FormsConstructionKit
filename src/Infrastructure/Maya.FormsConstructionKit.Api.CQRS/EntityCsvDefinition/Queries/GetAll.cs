using AutoMapper;
using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityCsvDefinition.Queries
{
    public class GetAllQuery : IRequest<csvDefinitionsResult>
    {
        public GetAllQuery()
        {

        }
    }

    public class GetAllHandler : IRequestHandler<GetAllQuery, csvDefinitionsResult>
    {
        private readonly IMapper mapper;
        private readonly AppDataContext dataContext;
        private readonly ILogger<GetAllHandler> logger;

        public GetAllHandler(IMapper mapper, AppDataContext dataContext, ILogger<GetAllHandler> logger)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<csvDefinitionsResult> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Library.Repositories.EntityCsvDefinition.AllAsync(this.dataContext)
                    .MapAsync(data => Task.FromResult(
                            data.Select(x => this.mapper.Map<Shared.Model.CsvDefinition>(x))
                        ))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{nameof(GetAllHandler)}");
                return csvDefinitionsResult.Failed(e);
            }
        }
    }
}
