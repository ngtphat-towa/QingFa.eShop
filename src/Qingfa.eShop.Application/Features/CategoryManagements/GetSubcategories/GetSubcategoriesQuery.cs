using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Responses;
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
        private readonly IApplicationDbContext _dbContext;

        public GetSubcategoriesQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw CoreException.NullArgument(nameof(dbContext));
        }

        public async Task<ResultValue<IReadOnlyList<BasicResponse<Guid>>>> Handle(GetSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the child categories using the DbContext
                var subcategories = await _dbContext.Categories
                    .Where(c => c.ParentCategoryId == request.ParentCategoryId)
                    .ToListAsync(cancellationToken);

                // Map the entities to response models
                var response = subcategories.Adapt<IReadOnlyList<BasicResponse<Guid>>>();

                return ResultValue<IReadOnlyList<BasicResponse<Guid>>>.Success(response);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return ResultValue<IReadOnlyList<BasicResponse<Guid>>>.UnexpectedError(ex);
            }
        }
    }
}
