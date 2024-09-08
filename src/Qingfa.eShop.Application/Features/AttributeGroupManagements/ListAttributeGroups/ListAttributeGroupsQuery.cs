using Ardalis.GuardClauses;

using Mapster;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeGroupManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.ListAttributeGroups
{
    public record ListAttributeGroupsQuery : IQuery<PaginatedList<AttributeGroupResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public List<Guid>? Ids { get; set; }
        public string SortField { get; set; } = "GroupName";
        public bool SortDescending { get; set; }
    }
    public class ListAttributeGroupsQueryHandler : IQueryHandler<ListAttributeGroupsQuery, PaginatedList<AttributeGroupResponse>>
    {
        private readonly IProductAttributeGroupRepository _repository;

        public ListAttributeGroupsQueryHandler(IProductAttributeGroupRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<PaginatedList<AttributeGroupResponse>>> Handle(ListAttributeGroupsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                Guard.Against.NegativeOrZero(request.PageNumber, nameof(request.PageNumber));
                Guard.Against.NegativeOrZero(request.PageSize, nameof(request.PageSize));

                // Create a specification based on the query parameters
                var specification = new ProductAttributeGroupSpecification(
                    name: request.Name,
                    description: request.Description,
                    ids: request.Ids,
                    createdBy: request.CreatedBy
                );

                // Get total count of items matching the specification
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

                // Fetch the data with the specification
                var attributeGroups = await _repository.FindBySpecificationAsync(specification, cancellationToken);

                // Map entities to response DTOs
                var attributeGroupResponses = attributeGroups.Adapt<List<AttributeGroupResponse>>();

                // Create and return paginated list of responses
                var paginatedList = new PaginatedList<AttributeGroupResponse>(
                    attributeGroupResponses,
                    totalCount,
                    request.PageNumber,
                    request.PageSize
                );

                return Result<PaginatedList<AttributeGroupResponse>>.Success(paginatedList);
            }
            catch (Exception ex)
            {
                // Log exception if necessary
                return Result<PaginatedList<AttributeGroupResponse>>.UnexpectedError(ex);
            }
        }
    }
}
