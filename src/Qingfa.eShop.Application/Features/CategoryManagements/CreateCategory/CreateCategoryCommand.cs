using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Common.ValueObjects;

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

    internal class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateCategoryCommand, Result<Guid>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw CoreException.NullArgument(nameof(categoryRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw CoreException.NullArgument(nameof(unitOfWork));

        public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the parent category exists if ParentCategoryId is provided
                if (request.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentCategoryId.Value, cancellationToken);
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
                var categoryExists = await _categoryRepository.ExistsByNameAsync(request.Name, request.ParentCategoryId, cancellationToken);
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

                // Add the category to the repository
                await _categoryRepository.AddAsync(category, cancellationToken);

                // Commit the transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(category.Id);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return Result<Guid>.UnexpectedError(ex);
            }
        }

        private async Task<bool> IsCyclicDependency(Guid parentCategoryId, string newCategoryName, CancellationToken cancellationToken)
        {
            // Get all categories including the new category's potential parent
            var categories = await _categoryRepository.ListAllAsync(cancellationToken);
            var categoryDict = categories.ToDictionary(c => c.Id);

            // Find the parent category
            if (!categoryDict.TryGetValue(parentCategoryId, out var parentCategory))
            {
                return false;
            }

            // Check for cyclic dependency
            return HasCyclicDependency(categoryDict, parentCategory, newCategoryName);
        }

        private static bool HasCyclicDependency(Dictionary<Guid, Category> categoryDict, Category parentCategory, string newCategoryName)
        {
            var visited = new HashSet<Guid>();
            var stack = new Stack<Category>();
            stack.Push(parentCategory);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.ChildCategories.Any(c => c.Name == newCategoryName))
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
