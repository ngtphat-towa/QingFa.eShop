using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.DeleteCategory
{
    public record DeleteCategoryCommand : RequestType<Guid>, IRequest<Result>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository ?? throw CoreException.NullArgument(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw CoreException.NullArgument(nameof(unitOfWork));
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the category exists
                var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
                if (category == null)
                {
                    return Result.NotFound("Category", "The specified category does not exist.");
                }

                // Check if the category has child categories
                var childCategories = await _categoryRepository.GetChildCategoriesAsync(request.Id, cancellationToken);
                if (childCategories.Count > 0)
                {
                    return Result.InvalidOperation("Delete category", "Category cannot be deleted because it has child categories.");
                }

                // Delete the category
                await _categoryRepository.DeleteAsync(category, cancellationToken);

                // Commit the transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return Result.UnexpectedError(ex);
            }
        }
    }
}
