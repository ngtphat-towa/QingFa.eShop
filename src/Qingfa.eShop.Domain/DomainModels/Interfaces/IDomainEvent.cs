using MediatR;

namespace QingFa.EShop.Domain.DomainModels
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
        IDictionary<string, object> MetaData { get; }
    }
}
