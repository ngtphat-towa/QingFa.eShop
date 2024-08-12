using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents an identifier for CatalogSize.
    /// </summary>
    public class CatalogSizeId : ValueObject
    {
        public int Value { get; private set; }

        private CatalogSizeId(int value)
        {
            if (value <= 0) // Ensure positive ID values
                throw new ArgumentException("ID must be greater than zero.", nameof(value));

            Value = value;
        }

        public static CatalogSizeId Create(int value) { return new CatalogSizeId(value); } 

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
