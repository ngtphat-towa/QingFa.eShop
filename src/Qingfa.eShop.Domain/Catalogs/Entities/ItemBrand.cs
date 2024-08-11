using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class ItemBrand : Entity<BrandId>
    {
        public ItemBrand(BrandId id) : base(id)
        {
        }
    }
}
