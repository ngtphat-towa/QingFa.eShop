
namespace QingFa.EShop.Domain.Core.Events
{
    public abstract class BaseEvent : IDomainEvent
    {
        public abstract Guid Id { get; init; }
    }
}
