using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public class CatalogItemId : ValueObject
    {
        public long Value { get; }

        // Protected constructor for ValueObject
        protected CatalogItemId(long value)
        {
            if (value <= 0) throw new ArgumentException("CatalogItemId value must be greater than zero.", nameof(value));
            Value = value;
        }

        // Factory method for creating instances
        public static CatalogItemId Create(long value)
        {
            return new CatalogItemId(value);
        }

        // Equality check based on the Value property
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
