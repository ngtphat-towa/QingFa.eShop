using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed class CatalogTypeId : ValueObject
    {
        public int Value { get; }

        private CatalogTypeId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

#pragma warning disable CS0628
        private CatalogTypeId()
#pragma warning restore CS0628
        {
        }

        public static CatalogTypeId Create(int value)
        {
            return new CatalogTypeId(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
