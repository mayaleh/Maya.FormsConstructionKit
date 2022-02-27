using AutoMapper;
using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityCsvDefinition.Commands
{
    public class CreateCommand : IRequest<csvDefinitionResult>
    {
        public CreateCommand(Shared.Model.CsvDefinition csvDefinition)
        {
            CsvDefinition = csvDefinition;
        }

        public CsvDefinition CsvDefinition { get; }
    }

    public class CreateHanlder : IRequestHandler<CreateCommand, csvDefinitionResult>
    {
        private readonly IMapper mapper;
        private readonly AppDataContext dataContext;
        private readonly ILogger<CreateHanlder> logger;

        public CreateHanlder(IMapper mapper, AppDataContext dataContext, ILogger<CreateHanlder> logger)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<csvDefinitionResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var storageCsvDefinition = this.mapper.Map<Model.Storage.EntityCsvDefinition>(request.CsvDefinition);
                
                if (storageCsvDefinition == null)
                {
                    return csvDefinitionResult.Failed(new ArgumentNullException(nameof(storageCsvDefinition)));
                }

                return await Library.Repositories.EntityCsvDefinition.AddOneAsync(this.dataContext, storageCsvDefinition)
                    .MapAsync(x =>
                            Task.FromResult(this.mapper.Map<Shared.Model.CsvDefinition>(x))
                        )
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return csvDefinitionResult.Failed(e);
            }
        }
    }
}
