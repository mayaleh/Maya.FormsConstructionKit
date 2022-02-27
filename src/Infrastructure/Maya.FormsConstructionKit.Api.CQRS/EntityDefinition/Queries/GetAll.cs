using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityDefinition.Queries
{
    public class GetAllQuery : IRequest<entitiesFormsResult>
    {
    }

    public class GetAllHandler : IRequestHandler<GetAllQuery, entitiesFormsResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<GetAllHandler> logger;

        public GetAllHandler(AppDataContext dataContext, ILogger<GetAllHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<entitiesFormsResult> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Library.Repositories.EntityDefinition.AllAsync(this.dataContext)
                    .MapAsync(storageData => Task.FromResult(
                            storageData.Select(x => x.Map())
                        ))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{nameof(GetAllHandler)}; Failled to read All {nameof(Model.Storage.EntityDefinition)} from the storage");
                return entitiesFormsResult.Failed(e);
            }
        }
    }
}
