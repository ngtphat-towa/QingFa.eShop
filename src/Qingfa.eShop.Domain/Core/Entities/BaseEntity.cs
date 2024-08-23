using QingFa.EShop.Domain.Core.Events;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QingFa.EShop.Domain.Core.Entities
{
    /// <summary>
    /// Represents a base entity with a unique identifier and domain event handling.
    /// </summary>
    /// <typeparam name="IKey">The type of the entity's unique identifier.</typeparam>
    public abstract class BaseEntity<IKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{IKey}"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the entity.</param>
        protected BaseEntity(IKey id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{IKey}"/> class. Used by ORM tools.
        /// </summary>
        protected BaseEntity()
        {
            Id = default!;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public IKey Id { get; protected set; }

        private readonly List<BaseEvent> _domainEvents = new();

        /// <summary>
        /// Gets the collection of domain events associated with the entity.
        /// </summary>
        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => new ReadOnlyCollection<BaseEvent>(_domainEvents);

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        public void AddDomainEvent(BaseEvent domainEvent)
        {
            if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to remove.</param>
        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events from the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
