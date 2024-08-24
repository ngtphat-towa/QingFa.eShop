using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Domain.Catalogs.Repositories.Attributes
{
    public interface IProductAttributeRepository : IGenericRepository<ProductAttribute, Guid>
    {
        /// <summary>
        /// Checks if an attribute with the specified name exists within a given attribute group.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="attributeGroupId">The optional attribute group ID to filter by.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if the attribute exists; otherwise, false.</returns>
        Task<bool> ExistsByNameAsync(string name, Guid? attributeGroupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if the attribute with the specified ID is in use by other entities.
        /// </summary>
        /// <param name="attributeId">The ID of the attribute to check.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if the attribute is in use; otherwise, false.</returns>
        Task<bool> IsInUseAsync(Guid attributeId, CancellationToken cancellationToken = default);
    }
}
