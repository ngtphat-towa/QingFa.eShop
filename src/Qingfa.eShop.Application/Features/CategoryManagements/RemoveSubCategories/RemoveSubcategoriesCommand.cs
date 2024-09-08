using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace QingFa.EShop.Application.Features.CategoryManagements.RemoveSubCategories
{
    public record RemoveSubcategoriesCommand : IRequest<Result>
    {
        public Guid ParentCategoryId { get; init; }
        public List<Guid> SubcategoryIds { get; init; } = new();
    }

    public class RemoveSubcategoriesCommandHandler : IRequestHandler<RemoveSubcategoriesCommand, Result>
    {
        private readonly IApplicationDbProvider _dbProvider;

        public RemoveSubcategoriesCommandHandler(IApplicationDbProvider dbProvider)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
        }

        public async Task<Result> Handle(RemoveSubcategoriesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the parent category
                var parentCategory = await _dbProvider.Categories
                    .Include(c => c.ChildCategories)
                    .FirstOrDefaultAsync(c => c.Id == request.ParentCategoryId, cancellationToken);

                if (parentCategory == null)
                {
                    return Result.NotFound(nameof(Category), "The specified parent category does not exist.");
                }

                // Fetch the child categories that are to be removed
                var subcategoryIds = request.SubcategoryIds.ToHashSet();
                var childCategories = await _dbProvider.Categories
                    .Where(c => subcategoryIds.Contains(c.Id))
                    .ToListAsync(cancellationToken);

                var validChildCategories = childCategories.Where(c => subcategoryIds.Contains(c.Id)).ToList();
                var invalidSubcategoryIds = subcategoryIds.Except(validChildCategories.Select(c => c.Id)).ToList();

                if (invalidSubcategoryIds.Any())
                {
                    return Result.NotFound(nameof(Category), $"The following subcategories do not exist: {string.Join(", ", invalidSubcategoryIds)}.");
                }

                // Remove child categories from the parent category
                foreach (var childCategory in validChildCategories)
                {
                    if (parentCategory.ChildCategories.Any(c => c.Id == childCategory.Id))
                    {
                        parentCategory.ChildCategories.Remove(childCategory);
                    }
                }

                // Update the parent category in the database
                _dbProvider.Categories.Update(parentCategory);

                // Commit the transaction
                await _dbProvider.SaveChangesAsync(cancellationToken);

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
