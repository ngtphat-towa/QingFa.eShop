using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using QingFa.EShop.Domain.Commons.ValueObjects;

namespace QingFa.EShop.Infrastructure.Persistence.Converters
{
    internal class StrongIdValueConverter<TId> : ValueConverter<TId, int>
    where TId : IdValueObject
    {
        public StrongIdValueConverter()
            : base(
                id => id.Value,
                value => (TId)Activator.CreateInstance(typeof(TId), value)!)
        {
        }
    }
}
