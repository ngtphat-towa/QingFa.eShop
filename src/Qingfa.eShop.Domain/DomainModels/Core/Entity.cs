using System.Text.Json.Serialization;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels.Core
{
    /// <summary>
    /// Serves as a base class for domain model entities, providing common properties such as Id, CreatedAt, and UpdatedAt timestamps,
    /// as well as methods for managing domain events. Implements equality comparison based on the entity's identifier.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier. Must be non-nullable.</typeparam>
    public abstract class Entity<TId> : IEntity<TId>, IHasDomainEvent, IEquatable<Entity<TId>> where TId : notnull
    {
        private readonly HashSet<IDomainEvent> _domainEvents = new HashSet<IDomainEvent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TId}"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity. This value is required to initialize the entity.</param>
        protected Entity(TId id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        /// <remarks>
        /// This property is set during entity initialization and is immutable to reflect the creation time accurately.
        /// </remarks>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        /// <remarks>
        /// This property is initially null and is updated whenever the entity is modified.
        /// </remarks>
        public DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        /// <remarks>
        /// Useful for auditing to track who initially created the entity.
        /// </remarks>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last updated the entity.
        /// </summary>
        /// <remarks>
        /// Useful for auditing to track who last modified the entity.
        /// </remarks>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Gets a read-only list of domain events associated with the entity.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

        /// <summary>
        /// Adds a domain event to the entity's list of domain events.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Removes a domain event from the entity's list of domain events.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
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
        /// Updates the timestamp indicating when the entity was last modified.
        /// </summary>
        /// <remarks>
        /// Sets the <see cref="UpdatedAt"/> property to the current UTC time. This method should be called whenever the entity is modified.
        /// </remarks>
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        #region Equality Members

        /// <summary>
        /// Determines whether the specified <see cref="Entity{TId}"/> is equal to the current <see cref="Entity{TId}"/>.
        /// </summary>
        /// <param name="other">The <see cref="Entity{TId}"/> to compare with the current <see cref="Entity{TId}"/>.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Entity{TId}"/> is equal to the current <see cref="Entity{TId}"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Entity<TId>? other)
        {
            // Check for null and compare reference equality
            if (other == null) return false;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="Entity{TId}"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="Entity{TId}"/>.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current <see cref="Entity{TId}"/>; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Entity<TId>);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="Entity{TId}"/>.
        /// </returns>
        public override int GetHashCode()
        {
            // Use the hash code of the Id for equality comparison
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        #endregion
    }
}
