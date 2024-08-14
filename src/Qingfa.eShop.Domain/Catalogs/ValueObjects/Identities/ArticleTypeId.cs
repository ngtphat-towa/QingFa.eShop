using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects.Identities
{
    public sealed class ArticleTypeId : ValueObject
    {
        public long Value { get; }

        private ArticleTypeId(long value)
        {
            // Validate the value and throw an appropriate exception if invalid
            if (value <= 0)
                throw CoreExceptionFactory.CreateInvalidArgumentException(nameof(value));

            Value = value;
        }

        private ArticleTypeId()
        {
        }

        public static ArticleTypeId Create(long value)
        {
            return new ArticleTypeId(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
