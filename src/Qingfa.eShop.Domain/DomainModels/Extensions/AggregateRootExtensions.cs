using MediatR;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace Qingfa.EShop.Domain.DomainModels.Extensions
{
    public static class AggregateRootExtensions
    {
        public static async Task RelayAndPublishEvents(this IHasDomainEvent aggregateRoot, IPublisher publisher, CancellationToken cancellationToken = default)
        {
            // Create a mutable copy of the domain events
            var events = new List<IDomainEvent>(aggregateRoot.DomainEvents);

            // Clear the domain events from the aggregate root
            aggregateRoot.ClearDomainEvents();

            // Publish each domain event
            foreach (var domainEvent in events)
            {
                await publisher.Publish(new EventWrapper(domainEvent), cancellationToken);
            }
        }

    }
}