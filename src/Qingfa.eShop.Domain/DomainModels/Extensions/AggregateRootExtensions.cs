using MediatR;

using QingFa.EShop.Domain.DomainModels;

namespace Qingfa.EShop.Domain.DomainModels.Extensions
{
    public static class AggregateRootExtensions
    {
        public static async Task RelayAndPublishEvents(this IAggregateRoot aggregateRoot, IPublisher publisher, CancellationToken cancellationToken = default)
        {
            var @events = new IDomainEvent[aggregateRoot.DomainEvents.Count];
            aggregateRoot.DomainEvents.CopyTo(@events);
            aggregateRoot.DomainEvents.Clear();

            foreach (var @event in @events)
            {
                await publisher.Publish(new EventWrapper(@event), cancellationToken);
            }
        }
    }
}