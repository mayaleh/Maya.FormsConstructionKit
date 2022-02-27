namespace Maya.FormsConstructionKit.Api.CQRS.EntityDefinition.Commands
{

    public class CreateCommand : IRequest<entityFormResult>
    {
        public CreateCommand(Shared.Model.EntityForm entityForm)
        {
            EntityForm = entityForm;
        }

        public Shared.Model.EntityForm EntityForm { get; }
    }

    public class CreateHandler : IRequestHandler<CreateCommand, entityFormResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<CreateHandler> logger;

        public CreateHandler(AppDataContext dataContext, ILogger<CreateHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<entityFormResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entityDefinition = request.EntityForm.Map();
                return await Library.Repositories.EntityDefinition.AddOneAsync(this.dataContext, entityDefinition)
                    .MapAsync(x =>
                            Task.FromResult(x.Map())
                        )
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, $"{nameof(CreateCommand)}; Message: {e.Message}");
                return entityFormResult.Failed(e);
            }
        }
    }
}
