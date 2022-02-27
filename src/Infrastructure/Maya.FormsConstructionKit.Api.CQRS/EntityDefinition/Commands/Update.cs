namespace Maya.FormsConstructionKit.Api.CQRS.EntityDefinition.Commands
{
    public class UpdateCommand : IRequest<entityFormResult>
    {
        public UpdateCommand(Shared.Model.EntityForm entityForm)
        {
            EntityForm = entityForm;
        }

        public Shared.Model.EntityForm EntityForm { get; }
    }

    public class UpdateHandler : IRequestHandler<UpdateCommand, entityFormResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<UpdateHandler> logger;

        public UpdateHandler(AppDataContext dataContext, ILogger<UpdateHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<entityFormResult> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entityDefinition = request.EntityForm.Map();
                return await Library.Repositories.EntityDefinition.UpdateAsync(this.dataContext, entityDefinition)
                    .MapAsync(x =>
                            Task.FromResult(x.Map())
                        )
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, $"{nameof(UpdateCommand)}; Message: {e.Message}");
                return entityFormResult.Failed(e);
            }
        }
    }
}
