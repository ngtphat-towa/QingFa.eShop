using MediatR;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels.Extensions
{
    public static class AggregateRootExtensions
    {
        public static async Task RelayAndPublishEvents(this IHasDomainEvent aggregateRoot,
                                                       IPublisher publisher,
                                                       CancellationToken cancellationToken = default)
        {
            // Convert IReadOnlyList to List
            var events = aggregateRoot.DomainEvents.ToList();
            aggregateRoot.ClearDomainEvents();

            foreach (var @event in events)
            {
                await publisher.Publish(new EventWrapper(@event), cancellationToken);
            }
        }
    }
}
