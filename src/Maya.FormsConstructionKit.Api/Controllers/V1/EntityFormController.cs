using Maya.Ext.Func.Rop;
using Maya.FormsConstructionKit.Api.Library.Mappers.Shared.Model;
using Maya.FormsConstructionKit.Api.Model.Storage;
using Maya.FormsConstructionKit.Shared.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Maya.FormsConstructionKit.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EntityFormController : ControllerBase
    {
        private readonly ILogger<EntityFormController> logger;
        private readonly IMediator mediator;

        public EntityFormController(ILogger<EntityFormController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<DataResult<IEnumerable<EntityForm>>> GetAll()
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityDefinition.Queries.GetAllQuery())
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<IEnumerable<EntityForm>>($"Failled to load all froms. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<IEnumerable<EntityForm>>($"Failled to load all froms. Reasson: {e.Message}");
            }
        }

        [HttpGet("available")]
        public async Task<DataResult<IEnumerable<string>>> GetAvailableNames()
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityDefinition.Queries.GetAllQuery())
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success.Select(x => x.Name)) :
                    DataResult.Failure<IEnumerable<string>>($"Failled to load all froms. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<IEnumerable<string>>($"Failled to load all froms. Reasson: {e.Message}");
            }
        }

        [HttpGet("latest/{name}")]
        public async Task<DataResult<EntityForm>> GetOneLatest(string name)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityDefinition.Queries.GetOneLatestQuery(name))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<EntityForm>($"Failled to load the form. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<EntityForm>($"Failled to load the from. Reasson: {e.Message}");
            }
        }

        [HttpGet("{name}")]
        public async Task<DataResult<IEnumerable<EntityForm>>> GetOneVersions(string name)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityDefinition.Queries.GetOneVersionsQuery(name))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<IEnumerable<EntityForm>>($"Failled to load all versions of the froms. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<IEnumerable<EntityForm>>($"Failled to load all versions of the froms. Reasson: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<DataResult<EntityForm>> Add([FromBody] EntityForm entityForm)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityDefinition.Commands.CreateCommand(entityForm))
                    .ConfigureAwait(false);

                return result.IsSuccess ? 
                    DataResult.Success(entityForm) :
                    DataResult.Failure<EntityForm>($"Failled to add new Form. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<EntityForm>($"Failled to add new Form. Reasson: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<DataResult<EntityForm>> Update([FromBody] EntityForm entityForm)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityDefinition.Commands.UpdateCommand(entityForm))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(entityForm) :
                    DataResult.Failure<EntityForm>($"Failled to update new Form. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<EntityForm>($"Failled to update new Form. Reasson: {e.Message}");
            }
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            // TODO
            return Ok();
        }
    }
}
