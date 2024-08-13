using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects.Identities
{
    public sealed class ArticleTypeId : ValueObject
    {
        public long Value { get; }

        private ArticleTypeId(long value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

#pragma warning disable CS0628
        private ArticleTypeId()
#pragma warning restore CS0628
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
