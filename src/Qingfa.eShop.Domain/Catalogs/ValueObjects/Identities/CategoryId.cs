using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects.Identities
{
    public sealed class CategoryId : ValueObject
    {
        public int Value { get; }

        private CategoryId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

#pragma warning disable CS0628
        private CategoryId()
#pragma warning restore CS0628
        {
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
