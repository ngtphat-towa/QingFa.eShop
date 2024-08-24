using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Domain.Catalogs.Repositories.Attributes
{
    public interface IProductAttributeOptionRepository : IGenericRepository<ProductAttributeOption, Guid>
    {
        /// <summary>
        /// Checks if an option with the specified value exists within a given product attribute.
        /// </summary>
        /// <param name="optionValue">The value of the option.</param>
        /// <param name="productAttributeId">The optional product attribute ID to filter by.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if the option exists; otherwise, false.</returns>
        Task<bool> ExistsByOptionValueAsync(string optionValue, Guid? productAttributeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if the option with the specified ID is in use by other entities.
        /// </summary>
        /// <param name="optionId">The ID of the option to check.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if the option is in use; otherwise, false.</returns>
        Task<bool> IsInUseAsync(Guid optionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves options by their product attribute ID.
        /// </summary>
        /// <param name="productAttributeId">The ID of the product attribute to filter by.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of options associated with the given product attribute ID.</returns>
        Task<IReadOnlyList<ProductAttributeOption>> GetByProductAttributeIdAsync(Guid productAttributeId, CancellationToken cancellationToken = default);
    }
}
