using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Domain.Commons.ValueObjects
{
    public abstract class IdValueObject(int value) : ValueObject
    {
        public int Value { get; } = value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
