using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed record TagId
    {
        public int Value { get; }

        private TagId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

        public static TagId Create(int value)
        {
            return new TagId(value);
        }
    }
}
