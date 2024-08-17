namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    /// <summary>
    /// Defines an entity that can have associated domain events.
    /// </summary>
    public interface IHasDomainEvent
    {
        /// <summary>
        /// Gets a read-only list of domain events associated with the entity.
        /// </summary>
        /// <remarks>
        /// Domain events represent important actions or state changes related to the entity.
        /// This property is read-only to ensure that the list cannot be modified directly from outside the class.
        /// </remarks>
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Adds a domain event to the entity's list of domain events.
        /// </summary>
        /// <param name="eventItem">The domain event to add. This event will be tracked by the entity.</param>
        void AddDomainEvent(IDomainEvent eventItem);

        /// <summary>
        /// Removes a domain event from the entity's list of domain events.
        /// </summary>
        /// <param name="eventItem">The domain event to remove. This event will no longer be tracked by the entity.</param>
        void RemoveDomainEvent(IDomainEvent eventItem);

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        /// <remarks>
        /// This method is typically used after domain events have been processed, to reset the list.
        /// </remarks>
        void ClearDomainEvents();
    }
}
