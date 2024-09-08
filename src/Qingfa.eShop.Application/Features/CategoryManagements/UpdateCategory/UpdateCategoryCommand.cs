using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QingFa.EShop.Application.Features.CategoryManagements.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public Guid? ParentCategoryId { get; init; }
        public EntityStatus? Status { get; init; }
    }

    internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<UpdateCategoryCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw CoreException.NullArgument(nameof(dbProvider));
            _logger = logger ?? throw CoreException.NullArgument(nameof(logger));
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the category to be updated
                var category = await _dbProvider.Categories
                    .Include(c => c.ChildCategories)
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (category == null)
                {
                    return Result.NotFound(nameof(Category), "The specified category does not exist.");
                }

                // Check if the parent category exists if ParentCategoryId is provided
                if (request.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _dbProvider.Categories
                        .Include(c => c.ChildCategories)
                        .FirstOrDefaultAsync(c => c.Id == request.ParentCategoryId.Value, cancellationToken);

                    if (parentCategory == null)
                    {
                        return Result.NotFound(nameof(Category), "The specified parent category does not exist.");
                    }

                    // Check for cyclic dependency
                    var hasCyclicDependency = await IsCyclicDependencyAsync(request.Id, request.ParentCategoryId.Value, cancellationToken);
                    if (hasCyclicDependency)
                    {
                        return Result.Conflict(nameof(Category), "Assigning this parent category would create a cyclic dependency.");
                    }
                }

                // Check if a category with the same name already exists
                var categoryExists = await _dbProvider.Categories
                    .AsNoTracking()
                    .AnyAsync(c => EF.Functions.Like(c.Name, $"%{request.Name}%") &&
                                   (c.ParentCategoryId == request.ParentCategoryId ||
                                    (request.ParentCategoryId == null && c.ParentCategoryId == null)),
                               cancellationToken);

                if (categoryExists)
                {
                    return Result.Conflict(nameof(Category), "A category with this name already exists under the specified parent category.");
                }

                // Update the category properties
                category.Update(
                    request.Name,
                    request.Description,
                    request.ImageUrl,
                    request.ParentCategoryId
                );

                // Update the status if provided
                if (request.Status.HasValue)
                {
                    category.SetStatus(request.Status.Value);
                }

                // Commit the transaction
                await _dbProvider.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category.");
                return Result.UnexpectedError(ex);
            }
        }

        private async Task<bool> IsCyclicDependencyAsync(Guid categoryId, Guid newParentId, CancellationToken cancellationToken)
        {
            var visited = new HashSet<Guid>();
            var stack = new Stack<Guid>();
            stack.Push(newParentId);

            while (stack.Count > 0)
            {
                var currentId = stack.Pop();
                if (currentId == categoryId)
                {
                    return true;
                }

                var currentCategory = await _dbProvider.Categories
                    .Include(c => c.ChildCategories)
                    .FirstOrDefaultAsync(c => c.Id == currentId, cancellationToken);

                if (currentCategory == null)
                {
                    continue;
                }

                foreach (var child in currentCategory.ChildCategories)
                {
                    if (!visited.Contains(child.Id))
                    {
                        visited.Add(child.Id);
                        stack.Push(child.Id);
                    }
                }
            }

            return false;
        }
    }
}
