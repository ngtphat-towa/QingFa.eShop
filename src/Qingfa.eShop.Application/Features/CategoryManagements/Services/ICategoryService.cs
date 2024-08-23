using QingFa.EShop.Application.Features.CategoryManagements.Models;

namespace QingFa.EShop.Application.Features.CategoryManagements.Services
{
    /// <summary>
    /// Defines the contract for category management services, including operations for building category trees.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Builds a hierarchical tree of categories starting from a specified root category.
        /// </summary>
        /// <param name="rootCategoryId">The ID of the root category from which to start building the tree.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the root of the category tree.</returns>
        Task<TreeCategoryTransfer> GetCategoryTreeAsync(Guid rootCategoryId, CancellationToken cancellationToken = default);
    }
}
