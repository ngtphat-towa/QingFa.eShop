using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Models;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Domain.Catalogs.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category, Guid>
    {
        /// <summary>
        /// Checks if a category with the given name exists within the specified parent category.
        /// </summary>
        /// <param name="name">The name of the category to check for existence.</param>
        /// <param name="parentCategoryId">The ID of the parent category to check under, or null for top-level categories.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if a category with the given name exists under the specified parent category, otherwise false.</returns>
        Task<bool> ExistsByNameAsync(string name, Guid? parentCategoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all child categories for a given parent category ID.
        /// </summary>
        /// <param name="parentId">The ID of the parent category whose child categories are to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of child categories under the specified parent category.</returns>
        Task<IReadOnlyList<Category>> GetChildCategoriesAsync(Guid parentId, CancellationToken cancellationToken = default);


        /// <summary>
        /// Retrieves minimal details (ID, Name, and ParentCategoryId) for categories related to the specified root category.
        /// </summary>
        /// <param name="rootCategoryId">The ID of the root category for which to fetch related minimal category details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of minimal category details related to the specified root category.</returns>
        Task<IReadOnlyList<MinimalCategoryTransfer>> GetCategoriesMinimalAsync(Guid rootCategoryId, CancellationToken cancellationToken = default);
    }
}
