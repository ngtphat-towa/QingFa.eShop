using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects.Identities
{
    public class ImageDetailId : ValueObject
    {
        public long Value { get; private set; }

        private ImageDetailId(long value)
        {
            if (value <= 0) // Ensure positive ID values
                throw CoreExceptionFactory.CreateInvalidArgumentException(nameof(value));


            Value = value;
        }
#pragma warning disable CS0628
        private ImageDetailId()
#pragma warning restore CS0628
        {
        }

        public static ImageDetailId Create(long value) { return new ImageDetailId(value); }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
