using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects.Identities
{
    public sealed class CatalogBrandId : ValueObject
    {
        public long Value { get; }

        private CatalogBrandId(long value)
        {
            if (value <= 0) throw CoreExceptionFactory.CreateInvalidArgumentException(nameof(value));
            Value = value;
        }

#pragma warning disable CS0628
        private CatalogBrandId()
#pragma warning restore CS0628
        {
        }

        public static CatalogBrandId Create(long value)
        {
            return new CatalogBrandId(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
