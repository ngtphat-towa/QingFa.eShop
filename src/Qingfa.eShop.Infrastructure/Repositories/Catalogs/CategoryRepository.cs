using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Infrastructure.Persistence.Data;
using QingFa.EShop.Infrastructure.Repositories.Common;

namespace QingFa.EShop.Infrastructure.Repositories.Catalogs
{
    internal class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(EShopDbContext context) : base(context)
        {
        }
        public new async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(x => x.ChildCategories).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<bool> ExistsByNameAsync(string name, Guid? parentCategoryId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(c => EF.Functions.Like(c.Name, $"%{name}%") &&
                               (c.ParentCategoryId == parentCategoryId ||
                                (parentCategoryId == null && c.ParentCategoryId == null)),
                           cancellationToken);
        }

        public async Task<IReadOnlyList<Category>> GetChildCategoriesAsync(Guid parentId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.ParentCategoryId == parentId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<MinimalCategoryTransfer>> GetCategoriesMinimalAsync(Guid rootCategoryId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.Id == rootCategoryId || c.ParentCategoryId == rootCategoryId)
                .Include(x => x.ChildCategories)
                .Select(c => new MinimalCategoryTransfer
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentCategoryId = c.ParentCategoryId
                })
                .ToListAsync(cancellationToken);
        }
    }
}
