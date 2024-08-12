using MediatR;

namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
        IDictionary<string, object> MetaData { get; }
    }
}
