using System.Text.Json.Serialization;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a base class for entities in the domain model, providing common properties such as Id, CreatedAt, and UpdatedAt timestamps,
    /// as well as support for managing domain events.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public abstract class Entity<TId> : IEntity<TId>, IHasDomainEvent where TId : notnull
    {
        private readonly HashSet<IDomainEvent> _domainEvents = new HashSet<IDomainEvent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
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
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last updated the entity.
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Gets a read-only list of domain events associated with the entity.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Removes a domain event from the entity.
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
    }
}
