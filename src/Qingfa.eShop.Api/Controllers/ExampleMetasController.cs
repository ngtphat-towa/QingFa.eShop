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
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleMetasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExampleMetasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a paginated list of ExampleMetas with optional filtering and sorting.
        /// </summary>
        /// <param name="pageNumber">The page number for pagination. Default is 1.</param>
        /// <param name="pageSize">The number of items per page. Default is 10.</param>
        /// <param name="name">Optional filter by name.</param>
        /// <param name="createdBy">Optional filter by creator.</param>
        /// <param name="sortField">The field to sort by. Default is "Created".</param>
        /// <param name="sortDescending">Indicates if sorting should be in descending order. Default is false.</param>
        /// <returns>A paginated list of ExampleMetas.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Gets a paginated list of ExampleMetas with optional filtering and sorting.")]
        [ProducesResponseType(typeof(PaginatedList<ExampleMetaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null,
            [FromQuery] string? createdBy = null,
            [FromQuery] string sortField = "Created",
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

        /// <summary>
        /// Gets an ExampleMeta by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ExampleMeta.</param>
        /// <returns>An ExampleMeta if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets an ExampleMeta by its unique identifier.")]
        [ProducesResponseType(typeof(ExampleMetaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetExampleMetaByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a new ExampleMeta.
        /// </summary>
        /// <param name="request">The details of the ExampleMeta to create.</param>
        /// <returns>201 Created response with the location of the newly created ExampleMeta.</returns>
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
                // Return 201 Created status with location of the new resource
                return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
            }

            return BadRequest(new { Errors = result.Errors });
        }

        /// <summary>
        /// Updates an existing ExampleMeta.
        /// </summary>
        /// <param name="id">The unique identifier of the ExampleMeta to update.</param>
        /// <param name="request">The updated details of the ExampleMeta.</param>
        /// <returns>204 No Content if the update is successful; otherwise, a 400 Bad Request response.</returns>
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

            if (result.Succeeded)
                return NoContent();

            return BadRequest(new { Errors = result.Errors });
        }

        /// <summary>
        /// Deletes an ExampleMeta by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ExampleMeta to delete.</param>
        /// <returns>204 No Content if the deletion is successful; otherwise, a 404 Not Found response.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an ExampleMeta by its unique identifier.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteExampleMetaCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command);

            if (result.Succeeded)
                return NoContent();

            return NotFound(new { Errors = result.Errors });
        }
    }
}
