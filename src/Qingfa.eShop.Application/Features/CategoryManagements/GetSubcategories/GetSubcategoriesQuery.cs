using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.GetSubcategories
{
    public class GetSubcategoriesQuery : IRequest<ResultValue<IReadOnlyList<CategoryResponse>>>
    {
        public Guid ParentCategoryId { get; init; }
    }

    public class GetSubcategoriesQueryHandler : IRequestHandler<GetSubcategoriesQuery, ResultValue<IReadOnlyList<CategoryResponse>>>
    {
        private readonly ICategoryRepository _repository;

        public GetSubcategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ResultValue<IReadOnlyList<CategoryResponse>>> Handle(GetSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            var subcategories = await _repository.GetChildCategoriesAsync(request.ParentCategoryId, cancellationToken);

            var response = subcategories.Adapt<IReadOnlyList<CategoryResponse>>();
            return ResultValue<IReadOnlyList<CategoryResponse>>.Success(response);
        }
    }
}
