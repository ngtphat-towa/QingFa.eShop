using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QingFa.EShop.Application.Features.CategoryManagements.DeleteCategory
{
    public record DeleteCategoryCommand : RequestType<Guid>, IRequest<Result>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IApplicationDbProvider _dbProvider;

        public DeleteCategoryCommandHandler(IApplicationDbProvider dbProvider)
        {
            _dbProvider = dbProvider ?? throw CoreException.NullArgument(nameof(dbProvider));
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Use IApplicationDbProvider to get the DbSet<Category>
                var categorySet = _dbProvider.Categories;

                // Check if the category exists
                var category = await categorySet
                    .Include(c => c.ChildCategories)
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (category == null)
                {
                    return Result.NotFound("Category", "The specified category does not exist.");
                }

                // Check if the category has child categories
                if (category.ChildCategories.Any())
                {
                    return Result.InvalidOperation("DeleteCategory", "Category cannot be deleted because it has child categories.");
                }

                // Remove the category
                categorySet.Remove(category);

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
