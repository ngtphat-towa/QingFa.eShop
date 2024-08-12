using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed record CatalogBrandId
    {
        public int Value { get; }

        private CatalogBrandId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

        private CatalogBrandId()
        {

        }
        public static CatalogBrandId Create(int value)
        {
            return new CatalogBrandId(value);
        }
    }
}
