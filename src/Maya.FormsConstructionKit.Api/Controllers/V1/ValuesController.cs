using System.Dynamic;
using Maya.FormsConstructionKit.Api.Model.Storage;
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
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;
        private readonly IMediator mediator;

        public ValuesController(ILogger<ValuesController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("{entityName}")]
        public async Task<DataResult<EntityData>> GetValues([FromRoute] string entityName)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityValue.Queries.GetValuesQuery(entityName))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<EntityData>($"Failled to load data. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<EntityData>($"Failled to load data. Reasson: {e.Message}");
            }
        }

        [HttpGet("csv/{entityName}/{csvDefinitionId}")]
        public async Task<IActionResult> GetCsv([FromRoute] string entityName, string csvDefinitionId)
        {
            try
            {
                var encodingName = "UTF-8";
                var contentType = "application/vnd.ms-excel";
                var csvResult = await this.mediator.Send(new CQRS.EntityValue.Queries.GetValuesCsvQuery(entityName, csvDefinitionId))
                    .ConfigureAwait(false);

                if (csvResult.IsFailure)
                    return this.BadRequest(CommandResult.Failure(csvResult.Failure.Message));

                var fileName = $"export_{entityName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.csv";

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var encoding = System.Text.Encoding.GetEncoding(encodingName);

                return File(encoding.GetBytes(csvResult.Success), contentType, fileName);
            }
            catch (Exception e)
            {
                return this.BadRequest(CommandResult.Failure($"Failled to load data. Reasson: {e.Message}"));
            }
        }

        [HttpPut("{entityName}")]
        public async Task<DataResult<object>> Add([FromRoute] string entityName, [FromBody] object data)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityValue.Commands.AddValueCommand(entityName, data))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<object>($"Failled to add new value. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<object>($"Failled to add new value. Reasson: {e.Message}");
            }
        }

        [HttpPost("{entityName}")]
        public async Task<DataResult<object>> Update([FromRoute] string entityName, [FromBody] object data)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityValue.Commands.UpdateValueCommand(entityName, data))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<object>($"Failled to update the row. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<object>($"Failled to update the row. Reasson: {e.Message}");
            }
        }

        [HttpPost("delete/{entityName}")]
        public async Task<CommandResult> Delete([FromRoute] string entityName, [FromBody] object data)
        {
            try
            {
                var result = await this.mediator.Send(new CQRS.EntityValue.Commands.DeleteValueCommand(entityName, data))
                    .ConfigureAwait(false);

                return result.IsSuccess ?
                    DataResult.Success(result.Success) :
                    DataResult.Failure<object>($"Failled to add new value. Reasson: {result.Failure.Message}");
            }
            catch (Exception e)
            {
                return DataResult.Failure<object>($"Failled to add new value. Reasson: {e.Message}");
            }
        }
    }
}
