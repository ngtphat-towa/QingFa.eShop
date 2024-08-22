using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.CategoryManagements.CreateCategory
{
    public record CreateCategoryCommand : IRequest<ResultValue<Guid>>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public Guid? ParentCategoryId { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResultValue<Guid>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository ?? throw CoreException.NullArgument(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw CoreException.NullArgument(nameof(unitOfWork));
        }

        public async Task<ResultValue<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the parent category exists if ParentCategoryId is provided
                if (request.ParentCategoryId.HasValue)
                {
                    var parentCategoryExists = await _categoryRepository.GetByIdAsync(request.ParentCategoryId.Value, cancellationToken);
                    if (parentCategoryExists == null)
                    {
                        return ResultValue<Guid>.NotFound(nameof(Category), "The specified parent category does not exist.");
                    }
                }

                // Check if a category with the same name already exists
                var categoryExists = await _categoryRepository.ExistsByNameAsync(request.Name, request.ParentCategoryId, cancellationToken);
                if (categoryExists)
                {
                    return ResultValue<Guid>.Conflict(nameof(Category), "A category with this name already exists under the specified parent category.");
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

                // Add the category to the repository
                await _categoryRepository.AddAsync(category, cancellationToken);

                // Commit the transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultValue<Guid>.Success(category.Id);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return ResultValue<Guid>.UnexpectedError(ex);
            }
        }
    }
}
