using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Application.Features.Common.Extensions;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Application.Features.CategoryManagements.GetCategoryById
{
    public record GetCategoryByIdQuery : RequestType<Guid>, IRequest<Result<CategoryResponse>>;
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryResponse>>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdQueryHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw CoreException.NullArgument(nameof(repository));
        }

        public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            if (ValidatorExtension.IsValidGuid(request.Id))
            {
                return Result<CategoryResponse>.InvalidOperation("GetCategoryByIdQuery", "Invalid ID provided.");
            }

            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
            {
                return Result<CategoryResponse>.NotFound("Category", request.Id.ToString());
            }

            var response = category.Adapt<CategoryResponse>();
            return Result<CategoryResponse>.Success(response);
        }
    }
}
