using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Interfaces;

namespace QingFa.EShop.Application.Features.CategoryManagements.CreateCategory
{
    public record CreateCategoryCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public Guid? ParentCategoryId { get; set; }
        public EntityStatus? Status { get; set; }
    }

    internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<CreateCategoryCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw CoreException.NullArgument(nameof(dbProvider));
            _logger = logger ?? throw CoreException.NullArgument(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the parent category exists if ParentCategoryId is provided
                if (request.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _dbProvider.Categories
                        .Include(c => c.ChildCategories)
                        .FirstOrDefaultAsync(c => c.Id == request.ParentCategoryId.Value, cancellationToken);

                    if (parentCategory == null)
                    {
                        return Result<Guid>.NotFound(nameof(Category), "The specified parent category does not exist.");
                    }

                    // Check for cyclic dependency
                    var hasCyclicDependency = await IsCyclicDependency(parentCategory.Id, request.Name, cancellationToken);
                    if (hasCyclicDependency)
                    {
                        return Result<Guid>.Conflict(nameof(Category), "Adding this category would create a cyclic dependency.");
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
                    return Result<Guid>.Conflict(nameof(Category), "A category with this name already exists under the specified parent category.");
                }

                // Convert SeoMetaTransfer to SeoMeta if provided
                var seoMeta = SeoMeta.Create(
                    request.SeoMeta.Title ?? string.Empty,
                    request.SeoMeta.Description ?? string.Empty,
                    request.SeoMeta.Keywords ?? string.Empty,
                    request.SeoMeta.CanonicalUrl,
                    request.SeoMeta.Robots);

                // Create the new category
                var category = Category.Create(
                    request.Name,
                    request.Description,
                    request.ImageUrl,
                    request.ParentCategoryId,
                    seoMeta);

                category.SetStatus(request.Status);

                // Add the category to the DbSet
                await _dbProvider.Categories.AddAsync(category, cancellationToken);

                // Commit the transaction
                await _dbProvider.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(category.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a category.");
                return Result<Guid>.UnexpectedError(ex);
            }
        }

        private async Task<bool> IsCyclicDependency(Guid parentCategoryId, string newCategoryName, CancellationToken cancellationToken)
        {
            var childCategories = await _dbProvider.Categories
                .Include(c => c.ChildCategories)
                .Where(c => c.Id == parentCategoryId)
                .SelectMany(c => c.ChildCategories)
                .ToListAsync(cancellationToken);

            return HasCyclicDependency(childCategories, newCategoryName);
        }

        private static bool HasCyclicDependency(IReadOnlyList<Category> childCategories, string newCategoryName)
        {
            var visited = new HashSet<Guid>();
            var stack = new Stack<Category>(childCategories);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.Name == newCategoryName)
                {
                    return true;
                }
                foreach (var child in current.ChildCategories)
                {
                    if (!visited.Contains(child.Id))
                    {
                        visited.Add(child.Id);
                        stack.Push(child);
                    }
                }
            }

            return false;
        }
    }
}
