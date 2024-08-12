using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed class CatalogCategoryId : ValueObject
    {
        public int Value { get; }

        private CatalogCategoryId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

#pragma warning disable CS0628
        private CatalogCategoryId()
#pragma warning restore CS0628
        {
        }

        public static CatalogCategoryId Create(int value)
        {
            return new CatalogCategoryId(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
