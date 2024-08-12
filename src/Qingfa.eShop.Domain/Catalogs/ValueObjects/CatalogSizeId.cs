using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed record CatalogSizeId
    {
        public int Value { get; }

        private CatalogSizeId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

        public static CatalogSizeId Create(int value)
        {
            return new CatalogSizeId(value);
        }
    }
}
