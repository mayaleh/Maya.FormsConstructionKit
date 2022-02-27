using Maya.FormsConstructionKit.Shared.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maya.FormsConstructionKit.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EntityCsvController : ControllerBase
    {
        private readonly ILogger<EntityCsvController> logger;
        private readonly IMediator mediator;

        public EntityCsvController(ILogger<EntityCsvController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<DataResult<IEnumerable<CsvDefinition>>> All()
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityCsvDefinition.Queries.GetAllQuery())
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<IEnumerable<CsvDefinition>>($"Failled to load csv definitions. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<IEnumerable<CsvDefinition>>($"Failled to load csv definitions. Reasson: {e.Message}");
            }
        }

        [HttpGet("{entityName}")]
        public async Task<DataResult<IEnumerable<CsvDefinition>>> AllForEntity([FromRoute] string entityName)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityCsvDefinition.Queries.GetByEntityNameQuery(entityName))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<IEnumerable<CsvDefinition>>($"Failled to load csv definitions. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<IEnumerable<CsvDefinition>>($"Failled to load csv definitions. Reasson: {e.Message}");
            }
        }

        [HttpGet("{entityName}/{csvDefinitionId}")]
        public async Task<IActionResult> OneForEntity([FromRoute] string entityName, string csvDefinitionId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<DataResult<CsvDefinition>> Add([FromBody] CsvDefinition csvDefinition)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityCsvDefinition.Commands.CreateCommand(csvDefinition))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<CsvDefinition>($"Failled to add new csv definition. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<CsvDefinition>($"Failled to add new csv definition. Reasson: {e.Message}");
            }
        }

        //Update
        //Delete
    }
}
