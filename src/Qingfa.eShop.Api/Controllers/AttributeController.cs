using MediatR;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeManagements.CreateAttribute;
using QingFa.EShop.Application.Features.AttributeManagements.DeleteAttribute;
using QingFa.EShop.Application.Features.AttributeManagements.GetAttributeById;
using QingFa.EShop.Application.Features.AttributeManagements.ListAttributes;
using QingFa.EShop.Application.Features.AttributeManagements.UpdateAttribute;
using QingFa.EShop.Application.Features.AttributeManagements.Models;
using Swashbuckle.AspNetCore.Annotations;
using QingFa.EShop.Application.Features.AttributeManagements.GetBasicAttributesByGroup;
using QingFa.EShop.API.Controllers;
using QingFa.EShop.Application.Features.AttributeManagements.AssignAttributeOptions;

namespace QingFa.EShop.Api.Controllers
{
    public class AttributeController : BaseController
    {
        private readonly IMediator _mediator;

        public AttributeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets a paginated list of Product Attributes with optional filtering and sorting.")]
        [ProducesResponseType(typeof(PaginatedList<ProductAttributeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? name = null,
            [FromQuery] string? attributeCode = null,
            [FromQuery] Guid? attributeGroupId = null,
            [FromQuery] bool? isFilterable = null,
            [FromQuery] bool? showToCustomers = null,
            [FromQuery] int? sortOrder = null,
            [FromQuery] string sortField = "Name",
            [FromQuery] bool sortDescending = false,
            [FromQuery] List<Guid>? ids = null)
        {
            var query = new ListProductAttributesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Name = name,
                SearchTerm = searchTerm,
                AttributeCode = attributeCode,
                AttributeGroupId = attributeGroupId,
                IsFilterable = isFilterable,
                ShowToCustomers = showToCustomers,
                SortOrder = sortOrder,
                SortField = sortField,
                SortDescending = sortDescending,
                Ids = ids
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a Product Attribute by its unique identifier.")]
        [ProducesResponseType(typeof(ProductAttributeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductAttributeByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Product Attribute.")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProductAttributeRequest request)
        {
            var command = request.Adapt<CreateProductAttributeCommand>();
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
        [SwaggerOperation(Summary = "Updates an existing Product Attribute.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductAttributeRequest request)
        {
            var command = request.Adapt<UpdateProductAttributeCommand>();
            command.Id = id;

            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a Product Attribute by its unique identifier.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductAttributeCommand(id);
            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpGet("by-group/basic")]
        [SwaggerOperation(Summary = "Gets a list of Product Attributes by Attribute Group with basic details.")]
        [ProducesResponseType(typeof(List<BasicProductAttributeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBasicAttributesByGroup(
        [FromQuery] Guid attributeGroupId,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string sortField = "Name",
        [FromQuery] bool sortDescending = false)
        {
            var query = new GetBasicAttributesByGroupQuery
            {
                AttributeGroupId = attributeGroupId,
                SearchTerm = searchTerm,
                SortField = sortField,
                SortDescending = sortDescending
            };

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle the exception, log it or return a proper error response
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/assign-options")]
        [SwaggerOperation(Summary = "Assigns options to a Product Attribute.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignOptions(Guid id, [FromBody] AssignAttributeOptionsRequest request)
        {
            var command = new AssignAttributeOptionsCommand
            (
                ProductAttributeId: id,
                AttributeOptionIds: request.AttributeOptionIds
            );

            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }
    }
}
