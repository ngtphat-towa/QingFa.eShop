using System.Text.Json.Serialization;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a base class for entities in the domain model, providing common properties such as Id, Created, and Updated timestamps.
    /// </summary>
    public abstract class Entity<TId> : IEntity, IHasDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        public int? CreatedBy { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last updated the entity.
        /// </summary>
        public int? UpdatedBy { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Gets the set of domain events associated with the entity.
        /// </summary>
        [JsonIgnore]
        IReadOnlyList<IDomainEvent> IHasDomainEvent.DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a domain event to the entity's event collection.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            if (eventItem == null) throw new ArgumentNullException(nameof(eventItem));
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Removes a domain event from the entity's event collection.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            if (eventItem == null) throw new ArgumentNullException(nameof(eventItem));
            _domainEvents.Remove(eventItem);
        }

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Gets the count of domain events.
        /// </summary>
        /// <returns>The number of domain events.</returns>
        public int GetDomainEventCount()
        {
            return _domainEvents.Count;
        }
        protected Entity(TId id = default!)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
        /// </summary>
        protected Entity()
        {
            Id = default!;
        }
    }
}
