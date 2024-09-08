using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Services;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.GetCategoryTree
{
    public class GetCategoryTreeQuery : IRequest<Result<TreeCategoryTransfer>>
    {
        public Guid ParentCategoryId { get; set; }
    }

    public class GetCategoryTreeQueryHandler : IRequestHandler<GetCategoryTreeQuery, Result<TreeCategoryTransfer>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryTreeService;

        public GetCategoryTreeQueryHandler(ICategoryRepository categoryRepository, ICategoryService categoryTreeService)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _categoryTreeService = categoryTreeService ?? throw new ArgumentNullException(nameof(categoryTreeService));
        }

        public async Task<Result<TreeCategoryTransfer>> Handle(GetCategoryTreeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the parent category exists
                var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentCategoryId, cancellationToken);
                if (parentCategory == null)
                {
                    return Result<TreeCategoryTransfer>.NotFound(nameof(Category), "The specified parent category does not exist.");
                }

                // Fetch the category tree
                var categoryTree = await _categoryTreeService.GetCategoryTreeAsync(request.ParentCategoryId, cancellationToken);
                return Result<TreeCategoryTransfer>.Success(categoryTree);
            }
            catch (Exception ex)
            {
                return Result<TreeCategoryTransfer>.UnexpectedError(ex);
            }
        }
    }

}
