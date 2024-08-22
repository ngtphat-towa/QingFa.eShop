using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Common.ValueObjects;

namespace QingFa.EShop.Application.Features.BrandManagements.Get
{
    public class ListBrandsQuery
        : IRequest<PaginatedList<BrandResponse>>
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
    public class ListBrandsQueryHandler(IBrandRepository repository)
        : IRequestHandler<ListBrandsQuery, PaginatedList<BrandResponse>>
    {
        private readonly IBrandRepository _repository = repository;

        public async Task<PaginatedList<BrandResponse>> Handle(ListBrandsQuery request, CancellationToken cancellationToken)
        {
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

            // Fetch the total count of items matching the specification
            var totalCount = await _repository.CountBySpecificationAsync(specification, cancellationToken);

            specification.ApplyPaging(request.PageNumber, request.PageSize);

            // Apply sorting
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

            // Apply paging parameters to the specification and fetch data
            var brands = await _repository.FindBySpecificationAsync(specification, cancellationToken);

            // Map entities to response DTOs
            var brandResponses = brands.Adapt<List<BrandResponse>>();

            // Create and return paginated list of responses
            var paginatedList = new PaginatedList<BrandResponse>(
                brandResponses,
                totalCount,
                request.PageNumber,
                request.PageSize);

            // Wrap the result 
            return paginatedList;
        }
    }
}