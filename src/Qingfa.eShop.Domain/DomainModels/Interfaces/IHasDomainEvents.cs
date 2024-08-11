namespace QingFa.EShop.Domain.DomainModels.Interfaces;

/// <summary>
/// Provides functionality for handling domain events.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets the collection of domain events associated with the aggregate root.
    /// </summary>
    /// <remarks>
    /// Domain events are used to capture and store important state changes within the
    /// aggregate. Implementations should ensure that domain events are handled properly
    /// and are available for processing or persistence.
    /// </remarks>
    HashSet<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Adds a domain event to the aggregate root's event collection.
    /// </summary>
    /// <param name="eventItem">The domain event to add.</param>
    void AddDomainEvent(IDomainEvent eventItem);

    /// <summary>
    /// Removes a domain event from the aggregate root's event collection.
    /// </summary>
    /// <param name="eventItem">The domain event to remove.</param>
    void RemoveDomainEvent(IDomainEvent eventItem);
}
