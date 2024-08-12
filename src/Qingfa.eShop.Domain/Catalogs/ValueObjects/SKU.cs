using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed class SKU : ValueObject
    {
        public string Code { get; private set; }

        public SKU(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("SKU cannot be null or empty.", nameof(value));

            Code = value;
        }

        public static SKU Create(string code)
        {
            return new SKU(code);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
