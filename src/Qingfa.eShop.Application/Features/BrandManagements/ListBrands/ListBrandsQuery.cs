using Ardalis.GuardClauses;

using Mapster;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Common.ValueObjects;

namespace QingFa.EShop.Application.Features.BrandManagements.ListBrands
{
    public class ListBrandsQuery
        : IQuery<PaginatedList<BrandResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public SeoMetaTransfer? SeoMeta { get; set; }
        public string? SortField { get; set; } = "Name";
        public bool SortDescending { get; set; }
        public IEnumerable<Guid>? Ids { get; set; }
    }
    internal class ListBrandsQueryHandler(IBrandRepository repository) 
        : IQueryHandler<ListBrandsQuery, PaginatedList<BrandResponse>>
    {
        private readonly IBrandRepository _repository = repository;

        public async Task<Result<PaginatedList<BrandResponse>>> Handle(ListBrandsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                Guard.Against.NegativeOrZero(request.PageNumber, nameof(request.PageNumber));
                Guard.Against.NegativeOrZero(request.PageSize, nameof(request.PageSize));

                // Convert SeoMetaTransfer to SeoMeta if not null
                var seoMeta = request.SeoMeta != null
                    ? SeoMeta.Create(
                        request.SeoMeta.Title ?? string.Empty,
                        request.SeoMeta.Description ?? string.Empty,
                        request.SeoMeta.Keywords ?? string.Empty,
                        request.SeoMeta.CanonicalUrl,
                        request.SeoMeta.Robots
                    )
                    : null;

                // Create a specification based on the query parameters
                var specification = new BrandSpecification(
                    name: request.Name,
                    ids: request.Ids,
                    createdBy: request.CreatedBy,
                    seoMeta: seoMeta
                );

                // Get total count of items matching the specification
                var totalCount = await _repository.CountBySpecificationAsync(specification, cancellationToken);

                // Apply paging and sorting to the specification
                specification.ApplyPaging(request.PageNumber, request.PageSize);

                if (!string.IsNullOrEmpty(request.SortField))
                {
                    if (request.SortDescending)
                    {
                        specification.ApplyOrderByDescending(request.SortField);
                    }
                    else
                    {
                        specification.ApplyOrderBy(request.SortField);
                    }
                }

                // Fetch the data with the specification
                var brands = await _repository.FindBySpecificationAsync(specification, cancellationToken);

                // Map entities to response DTOs
                var brandResponses = brands.Adapt<List<BrandResponse>>();

                // Create and return paginated list of responses
                var paginatedList = new PaginatedList<BrandResponse>(brandResponses, totalCount, request.PageNumber, request.PageSize);
                return Result<PaginatedList<BrandResponse>>.Success(paginatedList);
            }
            catch (Exception ex)
            {
                // Log exception if necessary
                return Result<PaginatedList<BrandResponse>>.UnexpectedError(ex);
            }
        }
    }
}