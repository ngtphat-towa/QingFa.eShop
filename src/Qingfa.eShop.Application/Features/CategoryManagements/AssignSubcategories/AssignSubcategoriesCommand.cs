using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Application.Core.Abstractions.Messaging;

namespace QingFa.EShop.Application.Features.CategoryManagements.AssignSubcategories
{
    public record AssignSubcategoriesCommand : ICommand
    {
        public Guid ParentCategoryId { get; init; }
        public List<Guid> SubcategoryIds { get; init; } = new List<Guid>();
    }

    internal class AssignSubcategoriesCommandHandler(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork,
        ILogger<AssignSubcategoriesCommandHandler> logger) : IRequestHandler<AssignSubcategoriesCommand, Result>
    {
        private readonly IApplicationDbContext _dbContext = dbContext ?? throw CoreException.NullArgument(nameof(dbContext));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw CoreException.NullArgument(nameof(unitOfWork));
        private readonly ILogger<AssignSubcategoriesCommandHandler> _logger = logger;

        public async Task<Result> Handle(AssignSubcategoriesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AssignSubcategoriesCommand with ParentCategoryId: {ParentCategoryId} and SubcategoryIds: {SubcategoryIds}",
                                    request.ParentCategoryId,
                                    string.Join(", ", request.SubcategoryIds));

            if (request.ParentCategoryId == Guid.Empty)
            {
                _logger.LogWarning("Invalid parent category ID: {ParentCategoryId}", request.ParentCategoryId);
                return Result.Failure("Invalid parent category ID.");
            }

            if (!request.SubcategoryIds.Any())
            {
                _logger.LogWarning("No subcategories selected.");
                return Result.Failure("No subcategories selected.");
            }

            try
            {
                // Fetch the parent category
                var parentCategory = await _dbContext.Categories
                    .Include(c => c.ChildCategories)
                    .FirstOrDefaultAsync(c => c.Id == request.ParentCategoryId, cancellationToken);

                if (parentCategory == null)
                {
                    _logger.LogWarning("Parent category not found: {ParentCategoryId}", request.ParentCategoryId);
                    return Result.NotFound(nameof(Category), "The specified parent category does not exist.");
                }

                // Fetch child categories
                var childCategories = await _dbContext.Categories
                    .Where(c => request.SubcategoryIds.Contains(c.Id))
                    .ToListAsync(cancellationToken);

                var subcategoryIds = request.SubcategoryIds.ToHashSet();
                var validChildCategories = childCategories.Where(c => subcategoryIds.Contains(c.Id)).ToList();
                var invalidSubcategoryIds = subcategoryIds.Except(validChildCategories.Select(c => c.Id)).ToList();

                if (invalidSubcategoryIds.Any())
                {
                    _logger.LogWarning("Invalid subcategory IDs: {InvalidSubcategoryIds}", string.Join(", ", invalidSubcategoryIds));
                    return Result.NotFound(nameof(Category), $"The following subcategories do not exist: {string.Join(", ", invalidSubcategoryIds)}.");
                }

                var existingSubcategoryIds = parentCategory.ChildCategories.Select(c => c.Id).ToHashSet();
                var newSubcategories = validChildCategories.Where(c => !existingSubcategoryIds.Contains(c.Id)).ToList();
                var subcategoriesToRemove = parentCategory.ChildCategories.Where(c => !subcategoryIds.Contains(c.Id)).ToList();

                // Remove old subcategories
                foreach (var subcategory in subcategoriesToRemove)
                {
                    parentCategory.ChildCategories.Remove(subcategory);
                    _logger.LogInformation("Removed child category ID: {ChildCategoryId} from parent category ID: {ParentCategoryId}",
                                           subcategory.Id,
                                           parentCategory.Id);
                }

                // Add new subcategories
                foreach (var childCategory in newSubcategories)
                {
                    if (await IsCyclicDependencyAsync(parentCategory, childCategory, cancellationToken))
                    {
                        _logger.LogWarning("Cyclic dependency detected when adding child category ID: {ChildCategoryId}", childCategory.Id);
                        return Result.Conflict("Child category", "Adding this category would create a cyclic dependency.");
                    }

                    parentCategory.ChildCategories.Add(childCategory);
                    _logger.LogInformation("Added child category ID: {ChildCategoryId} to parent category ID: {ParentCategoryId}",
                                           childCategory.Id,
                                           parentCategory.Id);
                }

                _logger.LogInformation("Updating parent category.");
                _dbContext.Categories.Update(parentCategory);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling AssignSubcategoriesCommand.");
                return Result.Conflict("Operation failed", "An unexpected error occurred.");
            }
        }

        private Task<bool> IsCyclicDependencyAsync(Category parentCategory, Category childCategory, CancellationToken cancellationToken)
        {
            var visited = new HashSet<Guid>();
            var stack = new Stack<Category>();
            stack.Push(parentCategory);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.Id == childCategory.Id)
                {
                    return Task.FromResult(true);
                }
                foreach (var sub in current.ChildCategories)
                {
                    if (!visited.Contains(sub.Id))
                    {
                        visited.Add(sub.Id);
                        stack.Push(sub);
                    }
                }
            }

            return Task.FromResult(false);
        }
    }
}
