using MediatR;
using Microsoft.AspNetCore.Mvc;
using QingFa.EShop.Application.ExampleMetas.Create;
using QingFa.EShop.Application.ExampleMetas.Delete;
using QingFa.EShop.Application.ExampleMetas.Update;
using QingFa.EShop.Application.ExampleMetas.Gets;
using QingFa.EShop.Application.ExampleMetas.Models;
using Swashbuckle.AspNetCore.Annotations;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Api.Models;

namespace QingFa.EShop.API.Controllers
{
    public class ExampleMetasController : BaseController
    {
        private readonly IMediator _mediator;

        public ExampleMetasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets a paginated list of ExampleMetas with optional filtering and sorting.")]
        [ProducesResponseType(typeof(PaginatedList<ExampleMetaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null,
            [FromQuery] string? createdBy = null,
            [FromQuery] string sortField = "Name",
            [FromQuery] bool sortDescending = false)
        {
            var query = new ListExampleMetasQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Name = name,
                CreatedBy = createdBy,
                SortField = sortField,
                SortDescending = sortDescending
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets an ExampleMeta by its unique identifier.")]
        [ProducesResponseType(typeof(ExampleMetaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetExampleMetaByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new ExampleMeta.")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateExampleMetaRequest request)
        {
            var command = new CreateExampleMetaCommand
            {
                Name = request.Name,
                CreatedBy = request.CreatedBy ?? "Unknown"
            };

            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Value },
                    result.Value
                );
            }

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing ExampleMeta.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExampleMetaRequest request)
        {
            var command = new UpdateExampleMetaCommand
            {
                Id = id,
                Name = request.Name,
                LastModifiedBy = request.LastModifiedBy ?? "Unknown"
            };

            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an ExampleMeta by its unique identifier.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteExampleMetaCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }
    }
}
