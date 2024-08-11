using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public record BrandId
    {
        public int Value { get; }
        private BrandId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(BrandId));
            Value = value;
        }
        public static BrandId Create(int value)
        {
            return new BrandId(value);
        }
    }
}
