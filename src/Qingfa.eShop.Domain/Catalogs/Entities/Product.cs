using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
