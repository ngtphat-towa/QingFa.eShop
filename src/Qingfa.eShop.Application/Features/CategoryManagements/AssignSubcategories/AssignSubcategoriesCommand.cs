using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.AssignSubcategories
{
    public record AssignSubcategoriesCommand : IRequest<Result>
    {
        public Guid ParentCategoryId { get; init; }
        public List<Guid> SubcategoryIds { get; init; } = new List<Guid>();
    }

    public class AssignSubcategoriesCommandHandler : IRequestHandler<AssignSubcategoriesCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignSubcategoriesCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(AssignSubcategoriesCommand request, CancellationToken cancellationToken)
        {
            // Input validation
            if (request.ParentCategoryId == Guid.Empty)
            {
                return Result.Failure("Invalid parent category ID.");
            }

            if (request.SubcategoryIds == null || !request.SubcategoryIds.Any())
            {
                return Result.Failure("No subcategories selected.");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentCategoryId, cancellationToken);
                if (parentCategory == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result.NotFound(nameof(Category), "The specified parent category does not exist.");
                }

                var childCategories = await _categoryRepository.FindBySpecificationAsync(
                    new CategorySpecification(ids: request.SubcategoryIds), cancellationToken
                );

                var subcategoryIds = request.SubcategoryIds.ToHashSet();
                var validChildCategories = childCategories.Where(c => subcategoryIds.Contains(c.Id)).ToList();
                var invalidSubcategoryIds = subcategoryIds.Except(validChildCategories.Select(c => c.Id)).ToList();

                if (invalidSubcategoryIds.Any())
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result.NotFound(nameof(Category), $"The following subcategories do not exist: {string.Join(", ", invalidSubcategoryIds)}.");
                }

                foreach (var childCategory in validChildCategories)
                {
                    if (IsCyclic(parentCategory, childCategory))
                    {
                        await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                        return Result.Failure("Adding this category would create a cyclic dependency.");
                    }

                    if (parentCategory.ChildCategories.All(c => c.Id != childCategory.Id))
                    {
                        parentCategory.AddChildCategory(childCategory);
                    }
                }

                await _categoryRepository.UpdateAsync(parentCategory, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                // Consider logging the exception here
                return Result.UnexpectedError(ex);
            }
        }

        private static bool IsCyclic(Category parent, Category child)
        {
            var visited = new HashSet<Guid>();
            var stack = new Stack<Category>();
            stack.Push(parent);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.Id == child.Id) return true;
                foreach (var sub in current.ChildCategories)
                {
                    if (!visited.Contains(sub.Id))
                    {
                        visited.Add(sub.Id);
                        stack.Push(sub);
                    }
                }
            }
            return false;
        }
    }
}
