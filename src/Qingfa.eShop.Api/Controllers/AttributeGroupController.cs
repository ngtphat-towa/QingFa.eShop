using MediatR;

using Microsoft.AspNetCore.Mvc;

using QingFa.EShop.API.Controllers;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeGroupManagements.CreateAttributeGroup;
using QingFa.EShop.Application.Features.AttributeGroupManagements.ListAttributeGroups;
using QingFa.EShop.Application.Features.AttributeGroupManagements.Models;

using Swashbuckle.AspNetCore.Annotations;
using QingFa.EShop.Application.Features.AttributeGroupManagements.GetBrandById;
using QingFa.EShop.Application.Features.AttributeGroupManagements.UpdateAttributeGroup;
using QingFa.EShop.Application.Features.AttributeGroupManagements.DeleteAttributeGroup;

namespace QingFa.EShop.Api.Controllers
{
    public class AttributeGroupController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Summary = "Gets a paginated list of attribute group with optional filtering and sorting.")]
        [ProducesResponseType(typeof(PaginatedList<AttributeGroupResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null,
            [FromQuery] string? description = null,
            [FromQuery] string? createdBy = null,
            [FromQuery] string? sortField = null,
            [FromQuery] bool sortDescending = false,
            [FromQuery] List<Guid>? ids = null)
        {
            var query = new ListAttributeGroupsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Name = name,
                Description = description,
                CreatedBy = createdBy,
                SortField = sortField ?? "GroupName",
                SortDescending = sortDescending,
                Ids = ids
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a attribute group by its unique identifier.")]
        [ProducesResponseType(typeof(AttributeGroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetAttributeGroupById { Id = id };
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new attribute group.")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAttributeGroupRequest request)
        {
            var command = new CreateAttributeGroupCommand
            {
                Name = request.Name,
                Description = request.Description ?? string.Empty,
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
        [SwaggerOperation(Summary = "Updates an existing attribute group.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateAttributeGroupRequest request)
        {
            var command = new UpdateAttributeGroupCommand
            {
                Id = id,
                Name = request.Name,
                Description = request.Description ?? string.Empty
            };

            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a attribute group by its unique identifier.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteAttributeGroupCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }
    }
}
