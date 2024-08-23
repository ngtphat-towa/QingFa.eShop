using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.RemoveSubCategories
{
    public record RemoveSubcategoriesCommand : IRequest<Result>
    {
        public Guid ParentCategoryId { get; init; }
        public List<Guid> SubcategoryIds { get; init; } = [];
    }
    public class RemoveSubcategoriesCommandHandler : IRequestHandler<RemoveSubcategoriesCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveSubcategoriesCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(RemoveSubcategoriesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the parent category
                var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentCategoryId, cancellationToken);
                if (parentCategory == null)
                {
                    return Result.NotFound(nameof(Category), "The specified parent category does not exist.");
                }

                // Fetch the child categories that are to be removed
                var childCategories = await _categoryRepository.FindBySpecificationAsync(
                    new CategorySpecification(ids: request.SubcategoryIds), cancellationToken
                );

                var subcategoryIds = request.SubcategoryIds.ToHashSet();
                var validChildCategories = childCategories.Where(c => subcategoryIds.Contains(c.Id)).ToList();
                var invalidSubcategoryIds = subcategoryIds.Except(validChildCategories.Select(c => c.Id)).ToList();

                if (invalidSubcategoryIds.Any())
                {
                    return Result.NotFound(nameof(Category), $"The following subcategories do not exist: {string.Join(", ", invalidSubcategoryIds)}.");
                }

                // Remove child categories from the parent category
                foreach (var childCategory in validChildCategories)
                {
                    // Ensure that the category is actually a child before attempting to remove it
                    if (parentCategory.ChildCategories.Any(c => c.Id == childCategory.Id))
                    {
                        parentCategory.RemoveChildCategory(childCategory.Id);
                    }
                }

                // Update the parent category in the repository
                await _categoryRepository.UpdateAsync(parentCategory, cancellationToken);

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
