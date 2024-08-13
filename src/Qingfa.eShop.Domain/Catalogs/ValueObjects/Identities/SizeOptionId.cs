using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects.Identities
{
    public class SizeOptionId : ValueObject
    {
        public long Value { get; private set; }

        private SizeOptionId(long value)
        {
            if (value <= 0) // Ensure positive ID values
                throw new ArgumentException("ID must be greater than zero.", nameof(value));

            Value = value;
        }
#pragma warning disable CS0628
        private SizeOptionId()
#pragma warning restore CS0628
        {
        }

        public static SizeOptionId Create(long value) { return new SizeOptionId(value); }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
