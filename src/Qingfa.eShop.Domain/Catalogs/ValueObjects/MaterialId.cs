using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed class MaterialId : ValueObject
    {
        public int Value { get; }

        private MaterialId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

        public static MaterialId Create(int value)
        {
            return new MaterialId(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
