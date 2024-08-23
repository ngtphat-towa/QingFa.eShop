using MediatR;
using Mapster;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Application.Features.CategoryManagements.ListCategories
{
    public record ListCategoriesQuery : IRequest<PaginatedList<CategoryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public SeoMetaTransfer? SeoMeta { get; set; }
        public List<EntityStatus>? Statuses { get; set; } = null;
        public string SortField { get; set; } = "Name";
        public bool SortDescending { get; set; }
        public List<Guid>? Ids { get; set; }
    }

    public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, PaginatedList<CategoryResponse>>
    {
        private readonly ICategoryRepository _repository;

        public ListCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<PaginatedList<CategoryResponse>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Create a specification based on the query parameters
                var specification = new CategorySpecification(
                         name: request.Name,
                         description: request.Description,
                         seoMeta: request.SeoMeta,
                         parentCategoryId: request.ParentCategoryId,
                         statuses: request.Statuses,
                         includeChildCategories: true,
                         includeParentCategory: true,
                         ids: request.Ids
                     );

                // Get total count of items without paging
                var totalCount = await _repository.CountBySpecificationAsync(specification, cancellationToken);

                // Apply paging parameters to the specification
                specification.ApplyPaging(request.PageNumber, request.PageSize);

                if (request.SortDescending)
                {
                    specification.ApplyOrderByDescending(request.SortField);
                }
                else
                {
                    specification.ApplyOrderBy(request.SortField);
                }
                // Fetch the data
                var categories = await _repository.FindBySpecificationAsync(specification, cancellationToken);

                // Map entities to response DTOs
                var categoryResponses = categories.Adapt<List<CategoryResponse>>();

                // Create and return paginated list of responses
                var paginatedList = new PaginatedList<CategoryResponse>(
                    categoryResponses,
                    totalCount,
                    request.PageNumber,
                    request.PageSize
                );

                return paginatedList;
            }
            catch (Exception ex)
            {
                // Log exception or handle it as necessary
                // For now, rethrow or return a failed result
                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
