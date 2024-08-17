using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public abstract class IdValueObject(int value) : ValueObject
    {
        public int Value { get; } = value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }

    public class BrandId(int value) : IdValueObject(value)
    {
    }

    public sealed class ProductId(int value) : IdValueObject(value)
    {
    }

    public sealed class CategoryId(int value) : IdValueObject(value)
    {
    }

    public sealed class AttributeGroupId(int value) : IdValueObject(value)
    {
    }
    
    public sealed class AttributeId(int value) : IdValueObject(value)
    {
    }

    public sealed class AttributeOptionId(int value) : IdValueObject(value)
    {
    }

    public sealed class CatalogVariantId(int value) : IdValueObject(value)
    {
    }
    public sealed class VariantValueId(int value) : IdValueObject(value)
    {
    }
}
