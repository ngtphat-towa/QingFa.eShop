using MediatR;

namespace QingFa.EShop.Domain.DomainModels.Extensions
{

    public class EventWrapper : INotification
    {
        public EventWrapper(IDomainEvent @event)
        {
            Event = @event ?? throw new ArgumentNullException(nameof(@event));
        }

        public IDomainEvent Event { get; }
    }
}
