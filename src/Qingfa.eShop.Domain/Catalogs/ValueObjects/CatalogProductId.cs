using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed class CatalogProductId : ValueObject
    {
        public Guid Value { get; private set; }

        // Private constructor for EF Core and internal use
        private CatalogProductId()
        {
        }

        // Public constructor for creation
        private CatalogProductId(Guid value)
        {
            Value = value;
        }

        // Factory method to create an instance
        public static CatalogProductId Create(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Invalid GUID value", nameof(value));
            }

            return new CatalogProductId(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
