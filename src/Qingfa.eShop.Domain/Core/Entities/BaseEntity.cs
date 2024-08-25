using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using QingFa.EShop.Domain.Core.Events;

namespace QingFa.EShop.Domain.Core.Entities
{
    /// <summary>
    /// Represents a base entity with a unique identifier and domain event handling.
    /// </summary>
    /// <typeparam name="IKey">The type of the entity's unique identifier.</typeparam>
    public abstract class BaseEntity<IKey> : IEquatable<BaseEntity<IKey>>
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
            // Default value to avoid using an uninitialized Id
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

        /// <summary>
        /// Determines whether the specified <see cref="BaseEntity{IKey}"/> is equal to the current <see cref="BaseEntity{IKey}"/>.
        /// </summary>
        /// <param name="other">The <see cref="BaseEntity{IKey}"/> to compare with the current <see cref="BaseEntity{IKey}"/>.</param>
        /// <returns>true if the specified <see cref="BaseEntity{IKey}"/> is equal to the current <see cref="BaseEntity{IKey}"/>; otherwise, false.</returns>
        public bool Equals(BaseEntity<IKey>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return EqualityComparer<IKey>.Default.Equals(Id, other.Id);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="BaseEntity{IKey}"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="BaseEntity{IKey}"/>.</param>
        /// <returns>true if the specified object is equal to the current <see cref="BaseEntity{IKey}"/>; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as BaseEntity<IKey>);
        }

        /// <summary>
        /// Serves as a hash function for the <see cref="BaseEntity{IKey}"/> type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="BaseEntity{IKey}"/>.</returns>
        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0; // Return 0 if Id is null
        }

        public static bool operator ==(BaseEntity<IKey>? left, BaseEntity<IKey>? right)
        {
            return EqualityComparer<BaseEntity<IKey>>.Default.Equals(left, right);
        }

        public static bool operator !=(BaseEntity<IKey>? left, BaseEntity<IKey>? right)
        {
            return !(left == right);
        }
    }
}
