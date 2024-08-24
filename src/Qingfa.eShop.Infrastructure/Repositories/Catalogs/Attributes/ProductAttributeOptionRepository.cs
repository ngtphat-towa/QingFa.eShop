using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Infrastructure.Persistence;

namespace QingFa.EShop.Infrastructure.Repositories.Catalogs.Attributes
{
    internal class ProductAttributeOptionRepository(EShopDbContext context) : GenericRepository<ProductAttributeOption, Guid>(context), IProductAttributeOptionRepository
    {

        /// <summary>
        /// Checks if an option with the specified value exists within a given product attribute.
        /// </summary>
        public async Task<bool> ExistsByOptionValueAsync(string optionValue, Guid? productAttributeId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ProductAttributeOption>()
                .AnyAsync(o => o.OptionValue == optionValue &&
                               (!productAttributeId.HasValue || o.ProductAttributeId == productAttributeId.Value),
                               cancellationToken);
        }


        /// <summary>
        /// Checks if the option with the specified ID is in use by other entities.
        /// </summary>
        public async Task<bool> IsInUseAsync(Guid optionId, CancellationToken cancellationToken = default)
        {
            // Check if the option is used in ProductVariantAttribute
            return await _context.Set<ProductVariantAttribute>()
                .AnyAsync(pva => pva.ProductAttributeOptionId == optionId, cancellationToken);
        }

        /// <summary>
        /// Retrieves options by their product attribute ID.
        /// </summary>
        public async Task<IReadOnlyList<ProductAttributeOption>> GetByProductAttributeIdAsync(Guid productAttributeId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ProductAttributeOption>()
                .Where(o => o.ProductAttributeId == productAttributeId)
                .ToListAsync(cancellationToken);
        }
    }
}
