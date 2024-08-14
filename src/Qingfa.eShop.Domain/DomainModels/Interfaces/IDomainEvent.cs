using MediatR;

namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; } 
        string CorrelationId { get; init; } 
        IDictionary<string, object> MetaData { get; } 

        void Flatten() { }
    }
}
