using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;

namespace QingFa.EShop.Application.Features.AttributeManagements.ListAttributes
{
    public record ListProductAttributesQuery : IRequest<PaginatedList<ProductAttributeResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? AttributeCode { get; set; }
        public Guid? AttributeGroupId { get; set; }
        public bool? IsFilterable { get; set; }
        public bool? ShowToCustomers { get; set; }
        public int? SortOrder { get; set; }
        public string SortField { get; set; } = "Name";
        public bool SortDescending { get; set; }
        public List<Guid>? Ids { get; set; }
        public string? SearchTerm { get; set; }
    }

    internal class ListProductAttributesQueryHandler : IRequestHandler<ListProductAttributesQuery, PaginatedList<ProductAttributeResponse>>
    {
        private readonly IProductAttributeRepository _repository;

        public ListProductAttributesQueryHandler(IProductAttributeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<PaginatedList<ProductAttributeResponse>> Handle(ListProductAttributesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Create a specification based on the query parameters
                var specification = new ProductAttributeSpecification(
                    name: request.Name,
                    attributeCode: request.AttributeCode,
                    attributeGroupId: request.AttributeGroupId,
                    isFilterable: request.IsFilterable,
                    showToCustomers: request.ShowToCustomers,
                    sortOrder: request.SortOrder,
                    includeAttributeOptions: true,
                    ids: request.Ids,
                    searchTerm: request.SearchTerm
                );

                // Get total count of items without paging
                var totalCount = await _repository.CountBySpecificationAsync(specification, cancellationToken);

                // Apply paging parameters to the specification
                specification.ApplyPaging(request.PageNumber, request.PageSize);

                // Apply sorting
                if (request.SortDescending)
                {
                    specification.ApplyOrderByDescending(request.SortField);
                }
                else
                {
                    specification.ApplyOrderBy(request.SortField);
                }

                // Fetch the data
                var attributes = await _repository.FindBySpecificationAsync(specification, cancellationToken);

                // Map entities to response DTOs
                var attributeResponses = attributes.Adapt<List<ProductAttributeResponse>>();

                // Create and return paginated list of responses
                var paginatedList = new PaginatedList<ProductAttributeResponse>(
                    attributeResponses,
                    totalCount,
                    request.PageNumber,
                    request.PageSize
                );

                return paginatedList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
