using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.UpdateCategory
{
    public record UpdateCategoryCommand : RequestType<Guid>, IRequest<Result>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public EntityStatus? Status { get; set; }
    }

    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the category exists
                var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
                if (category == null)
                {
                    return Result.NotFound(nameof(Category), "The specified category does not exist.");
                }

                // Check if the parent category exists if ParentCategoryId is provided
                if (request.ParentCategoryId.HasValue)
                {
                    var parentCategoryExists = await _categoryRepository.GetByIdAsync(request.ParentCategoryId.Value, cancellationToken);
                    if (parentCategoryExists == null)
                    {
                        return Result.NotFound(nameof(Category), "The specified parent category does not exist.");
                    }

                    // Check for cyclic dependency
                    if (IsCyclicDependency(request.Id, request.ParentCategoryId.Value, cancellationToken))
                    {
                        return Result.Conflict(nameof(Category), "Assigning this parent category would create a cyclic dependency.");
                    }
                }

                // Check if a category with the same name exists under the same parent category
                var categoryExists = await _categoryRepository.ExistsByNameAsync(request.Name, request.ParentCategoryId, cancellationToken);
                if (categoryExists)
                {
                    return Result.Conflict(nameof(Category), "A category with this name already exists under the specified parent category.");
                }

                // Update the category
                category.Update(
                    request.Name,
                    request.Description,
                    request.ImageUrl,
                    request.ParentCategoryId
                );

                // Check if the status is valid
                if (request.Status.HasValue)
                {
                    category.SetStatus(request.Status.Value);
                }

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

        private bool IsCyclicDependency(Guid categoryId, Guid newParentId, CancellationToken cancellationToken)
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

                var currentCategory = _categoryRepository.GetByIdAsync(currentId, cancellationToken).Result;
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
