using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public sealed class InventoryId : ValueObject
    {
        public int Value { get; }

        public InventoryId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

        public static InventoryId Create(int value)
        {
            return new InventoryId(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
