using MediatR;
using Microsoft.AspNetCore.Mvc;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Application.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using QingFa.EShop.Application.Features.BrandManagements.Update;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Application.Features.BrandManagements.CreateBrand;
using QingFa.EShop.Application.Features.BrandManagements.ListBrands;
using QingFa.EShop.Application.Features.BrandManagements.GetBrandById;
using QingFa.EShop.Application.Features.BrandManagements.DeleteBrand;

namespace QingFa.EShop.API.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets a paginated list of Brands with optional filtering and sorting.")]
        [ProducesResponseType(typeof(PaginatedList<BrandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null,
            [FromQuery] string? createdBy = null,
            [FromQuery] string? sortField = null,
            [FromQuery] bool sortDescending = false,
            [FromQuery] SeoMetaTransfer? seoMeta = null,
            [FromQuery] List<Guid>? id = null)
        {
            var query = new ListBrandsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Name = name,
                CreatedBy = createdBy,
                SortField = sortField,
                SortDescending = sortDescending,
                SeoMeta = seoMeta,
                Ids = id
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a Brand by its unique identifier.")]
        [ProducesResponseType(typeof(BrandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetBrandByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Brand.")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBrandRequest request)
        {
            var command = new CreateBrandCommand
            {
                Name = request.Name,
                Description = request.Description ?? string.Empty,
                SeoMeta = request.SeoMeta,
                LogoUrl = request.LogoUrl
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
        [SwaggerOperation(Summary = "Updates an existing Brand.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateBrandRequest request)
        {
            var command = new UpdateBrandCommand
            {
                Id = id,
                Name = request.Name,
                Description = request.Description ?? string.Empty,
                SeoMeta = request.SeoMeta,
                LogoUrl = request.LogoUrl
            };

            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a Brand by its unique identifier.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBrandCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Succeeded) return NoContent();

            return HandleResult(result);
        }
    }
}
