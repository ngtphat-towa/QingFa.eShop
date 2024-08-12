using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class CatalogType : Entity<CatalogTypeId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private CatalogType(CatalogTypeId catalogTypeId, string name, string description)
            : base(catalogTypeId)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Description = description;
        }

        public static CatalogType Create(CatalogTypeId catalogTypeId, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullArgument(nameof(name));
            return new CatalogType(catalogTypeId, name, description);
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected CatalogType()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
