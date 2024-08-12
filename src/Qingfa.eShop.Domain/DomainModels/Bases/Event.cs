using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    public abstract class Event : IDomainEvent
    {
        public string EventType => GetType().FullName ?? "UnknownEventType";

        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public string CorrelationId { get; init; } = string.Empty;
        public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();
        public abstract void Flatten();
    }
}
