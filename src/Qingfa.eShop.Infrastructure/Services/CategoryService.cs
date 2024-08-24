using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Application.Features.CategoryManagements.Services;
using QingFa.EShop.Domain.Catalogs.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        /// <inheritdoc/>
        public async Task<TreeCategoryTransfer> GetCategoryTreeAsync(Guid rootCategoryId, CancellationToken cancellationToken = default)
        {
            if (rootCategoryId == Guid.Empty)
            {
                throw new ArgumentException("Root category ID cannot be empty.", nameof(rootCategoryId));
            }

            // Fetch categories with minimal details
            var categories = await _categoryRepository.GetCategoriesMinimalAsync(rootCategoryId, cancellationToken);

            // Build a dictionary for fast access
            var categoryDict = categories.ToDictionary(c => c.Id);

            // Build the tree starting from the root category
            return BuildTree(categoryDict, rootCategoryId);
        }

        private TreeCategoryTransfer BuildTree(Dictionary<Guid, MinimalCategoryTransfer> categoryDict, Guid rootId)
        {
            if (!categoryDict.TryGetValue(rootId, out var rootCategory))
            {
                // Returning null is not ideal; consider throwing an exception or returning a specific result
                throw new KeyNotFoundException($"Category with ID {rootId} not found.");
            }

            var subCategories = categoryDict.Values
                .Where(c => c.ParentCategoryId == rootId)
                .Select(c => BuildTree(categoryDict, c.Id))
                .ToList();

            return new TreeCategoryTransfer
            {
                Id = rootCategory.Id,
                Name = rootCategory.Name,
                SubCategories = subCategories
            };
        }
    }
}
