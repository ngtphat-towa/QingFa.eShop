using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public class CatalogBrandId : ValueObject
    {
        public Guid Value { get; private set; }

        // Constructor for CatalogBrandId
        public CatalogBrandId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
