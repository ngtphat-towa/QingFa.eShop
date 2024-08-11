using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public class TypeId
    {
        public int Value { get; }
        private TypeId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(TypeId));
            Value = value;
        }
        public static TypeId Create(int value)
        {
            return new TypeId(value);
        }
    }
}
