using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public class CatalogItemId : ValueObject
    {
        public Guid Value { get; }

        // Protected constructor for ValueObject
        protected CatalogItemId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("CatalogItemId cannot be empty.", nameof(value));
            Value = value;
        }

        // Factory method for creating instances
        public static CatalogItemId Create(Guid value)
        {
            return new CatalogItemId(value);
        }

        // Static Factory Method for Default Initialization
        public static CatalogItemId CreateDefault()
        {
            return new CatalogItemId(Guid.NewGuid()); // Create a new GUID as the default value
        }

        // Equality check based on the Value property
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
