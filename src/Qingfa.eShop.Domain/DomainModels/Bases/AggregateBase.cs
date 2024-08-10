using System.Text.Json.Serialization;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a base class for aggregate root entities in the domain model,
    /// which includes support for domain events.
    /// </summary>
    public abstract class AggregateBase : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// Gets the set of domain events associated with the aggregate root.
        /// </summary>
        [JsonIgnore]
        public HashSet<IDomainEvent> DomainEvents { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateBase"/> class.
        /// </summary>
        protected AggregateBase()
        {
            DomainEvents = new HashSet<IDomainEvent>();
        }

        /// <summary>
        /// Adds a domain event to the aggregate root's event collection.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents.Add(eventItem);
        }

        /// <summary>
        /// Removes a domain event from the aggregate root's event collection.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents.Remove(eventItem);
        }
    }
}
