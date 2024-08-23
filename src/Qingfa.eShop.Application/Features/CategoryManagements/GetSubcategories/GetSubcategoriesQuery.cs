using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Responses;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Application.Features.CategoryManagements.GetSubcategories
{
    public class GetSubcategoriesQuery : IRequest<ResultValue<IReadOnlyList<BasicResponse<Guid>>>>
    {
        public Guid ParentCategoryId { get; init; }
    }

    public class GetSubcategoriesQueryHandler 
        : IRequestHandler<GetSubcategoriesQuery, ResultValue<IReadOnlyList<BasicResponse<Guid>>>>
    {
        private readonly ICategoryRepository _repository;

        public GetSubcategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw CoreException.NullArgument(nameof(repository));
        }

        public async Task<ResultValue<IReadOnlyList<BasicResponse<Guid>>>> Handle(GetSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            var subcategories = await _repository.GetChildCategoriesAsync(request.ParentCategoryId, cancellationToken);

            var response = subcategories.Adapt<IReadOnlyList<BasicResponse<Guid>>>();
            return ResultValue<IReadOnlyList<BasicResponse<Guid>>>.Success(response);
        }
    }
}
