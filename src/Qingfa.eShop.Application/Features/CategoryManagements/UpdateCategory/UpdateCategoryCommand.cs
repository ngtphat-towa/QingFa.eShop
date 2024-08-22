using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public EntityStatus? Status { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

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

                // TODO: check if the status is valid or not 
                category.SetStatus(request.Status);

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
