using MediatR;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels.Extensions;

public static class AggregateRootExtensions
{
    public static async Task RelayAndPublishEvents(this IHasDomainEvents aggregateEvent, IPublisher publisher, CancellationToken cancellationToken = default)
    {
        var @events = new IDomainEvent[aggregateEvent.DomainEvents.Count];
        aggregateEvent.DomainEvents.CopyTo(@events);
        aggregateEvent.DomainEvents.Clear();

        foreach (var @event in @events)
        {
            await publisher.Publish(new EventWrapper(@event), cancellationToken);
        }
    }
}