using MediatR;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeOptionManagements.CreateAttributeOption;
using QingFa.EShop.Application.Features.AttributeOptionManagements.ListAttributeOptions;
using QingFa.EShop.Application.Features.AttributeOptionManagements.Models;
using Swashbuckle.AspNetCore.Annotations;
using QingFa.EShop.API.Controllers;
using QingFa.EShop.Application.Features.AttributeOptionManagements.UpdateAttributeOption;
using QingFa.EShop.Application.Features.AttributeOptionManagements.DeleteAttributeOption;
using QingFa.EShop.Application.Features.AttributeOptionManagements.GetAttributeOptionById;

namespace QingFa.EShop.Api.Controllers
{
    public class AttributeOptionController : BaseController
    {
        private readonly IMediator _mediator;

        public AttributeOptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets a paginated list of Attribute Options with optional filtering and sorting.")]
        [ProducesResponseType(typeof(PaginatedList<AttributeOptionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? optionValue = null,
            [FromQuery] string? description = null,
            [FromQuery] bool? isDefault = null,
            [FromQuery] int? sortOrder = null,
            [FromQuery] Guid? productAttributeId = null,
            [FromQuery] string sortField = "OptionValue",
            [FromQuery] bool sortDescending = false,
            [FromQuery] List<Guid>? ids = null)
        {
            var query = new ListAttributeOptionsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OptionValue = optionValue,
                SearchTerm = searchTerm,
                Description = description,
                IsDefault = isDefault,
                SortOrder = sortOrder,
                ProductAttributeId = productAttributeId,
                SortField = sortField,
                SortDescending = sortDescending,
                Ids = ids
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets an Attribute Option by its unique identifier.")]
        [ProducesResponseType(typeof(AttributeOptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetAttributeOptionByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Attribute Option.")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAttributeOptionRequest request)
        {
            var command = request.Adapt<CreateAttributeOptionCommand>();
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
        [SwaggerOperation(Summary = "Updates an existing Attribute Option.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAttributeOptionRequest request)
        {
            var command = request.Adapt<UpdateAttributeOptionCommand>();
            command.Id = id;

            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an Attribute Option by its unique identifier.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteAttributeOptionCommand(id);
            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }
    }
}
