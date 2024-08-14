using ErrorOr;

using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Brands
{
    public sealed class CategoryId : ValueObject
    {
        public int Value { get; private set; }

        private CategoryId()
        {
        }
        private CategoryId(int value)
        {
            Value = value;
        }
        public static CategoryId Create(int value)
        {
            return new CategoryId(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
