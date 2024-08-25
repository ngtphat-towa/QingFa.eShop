using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Infrastructure.Persistence;

namespace QingFa.EShop.Infrastructure.Repositories.Catalogs.Attributes
{
    internal class ProductAttributeRepository(EShopDbContext context) 
        : GenericRepository<ProductAttribute, Guid>(context), IProductAttributeRepository
    {
        public async Task<bool> ExistsByNameAsync(
            string name,
            Guid? attributeGroupId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw CoreException.NullOrEmptyArgument(nameof(name));
            }

            var query = _dbSet.AsNoTracking().AsQueryable();

            if (attributeGroupId.HasValue)
            {
                query = query.Where(c => c.AttributeGroupId == attributeGroupId.Value);
            }

            // Use ToLower() to handle case-insensitive comparison
            return await query
                .AnyAsync(c => EF.Functions.Like(c.Name.ToLower(), name.ToLower()), cancellationToken);
        }

        public new async Task<ProductAttribute?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(a => a.AttributeOptions)
                .Include(a => a.VariantAttributes) // Include VariantAttributes to check associations
                .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<bool> IsInUseAsync(Guid attributeId, CancellationToken cancellationToken = default)
        {
            // Check if the attribute is associated with any ProductVariantAttributes
            var isInProductVariant = await _context.Set<ProductVariantAttribute>()
                .AnyAsync(pva => pva.ProductAttributeId == attributeId, cancellationToken);

            // Example: Check if the attribute is used in any other entities if applicable
            // Add any additional checks for other potential uses here

            return isInProductVariant;
        }
    }
}
