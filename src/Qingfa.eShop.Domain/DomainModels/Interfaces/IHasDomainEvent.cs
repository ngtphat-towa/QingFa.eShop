namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    /// <summary>
    /// Represents an entity that can have domain events.
    /// </summary>
    public interface IHasDomainEvent
    {
        /// <summary>
        /// Gets a read-only list of domain events associated with the entity.
        /// </summary>
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        void AddDomainEvent(IDomainEvent eventItem);

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        void RemoveDomainEvent(IDomainEvent eventItem);

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        void ClearDomainEvents();
    }
}
