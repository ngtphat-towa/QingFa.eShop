using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using QingFa.EShop.API.Controllers;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.AssignSubcategories;
using QingFa.EShop.Application.Features.CategoryManagements.CreateCategory;
using QingFa.EShop.Application.Features.CategoryManagements.DeleteCategory;
using QingFa.EShop.Application.Features.CategoryManagements.GetCategoryById;
using QingFa.EShop.Application.Features.CategoryManagements.GetCategoryTree;
using QingFa.EShop.Application.Features.CategoryManagements.GetSubcategories;
using QingFa.EShop.Application.Features.CategoryManagements.ListCategories;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Application.Features.CategoryManagements.UpdateCategories;
using QingFa.EShop.Application.Features.Common.SeoInfo;

using Swashbuckle.AspNetCore.Annotations;

namespace QingFa.EShop.Api.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets a paginated list of Categories with optional filtering and sorting.")]
        [ProducesResponseType(typeof(PaginatedList<CategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null,
            [FromQuery] string? createdBy = null,
            [FromQuery] Guid? parentCategoryId = null,
            [FromQuery] string? sortField = "Name",
            [FromQuery] bool sortDescending = false,
            [FromQuery] SeoMetaTransfer? seoMeta = null,
            [FromQuery] Guid? id = null)
        {
            var query = new ListCategoriesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Name = name,
                CreatedBy = createdBy,
                SeoMeta = seoMeta,
                ParentCategoryId = parentCategoryId,
                SortField = sortField ?? "Name",
                SortDescending = sortDescending,
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a Category by its unique identifier.")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Category.")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var command = request.Adapt<CreateCategoryCommand>();
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
        [SwaggerOperation(Summary = "Updates an existing Category.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateCategoryRequest request)
        {
            var command = request.Adapt<UpdateCategoryCommand>();
            command.Id = id; // Ensure the command has the correct ID

            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a Category by its unique identifier.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpPost("{id}/assign-subcategories")]
        [SwaggerOperation(Summary = "Assigns subcategories to a parent category.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignSubcategories(Guid id, [FromBody] AssignSubcategoriesRequest request)
        {
            if (request.ParentCategoryId != id)
            {
                return BadRequest("Parent category ID in the URL does not match the request body.");
            }

            var command = request.Adapt<AssignSubcategoriesCommand>();
            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpGet("{id}/tree")]
        [SwaggerOperation(Summary = "Gets the category tree for a given parent category.")]
        [ProducesResponseType(typeof(TreeCategoryTransfer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryTree(Guid id)
        {
            var query = new GetCategoryTreeQuery { ParentCategoryId = id };
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

        [HttpGet("{id}/subcategories")]
        [SwaggerOperation(Summary = "Gets the subcategories for a given parent category.")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubcategories(Guid id)
        {
            var query = new GetSubcategoriesQuery { ParentCategoryId = id };
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

    }
}
