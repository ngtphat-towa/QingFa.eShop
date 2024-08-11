using System.Text.Json.Serialization;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels.Bases;

/// <summary>
/// Represents a base class for aggregate root entities in the domain model,
/// which includes support for domain events.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>, IHasDomainEvents 
{
    /// <summary>
    /// Gets the set of domain events associated with the aggregate root.
    /// </summary>
    [JsonIgnore]
    public HashSet<IDomainEvent> DomainEvents { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// </summary>
    protected AggregateRoot(TId id) : base(id)
    {
        DomainEvents = [];
    }

    /// <summary>
    /// Adds a domain event to the aggregate root's event collection.
    /// </summary>
    /// <param name="eventItem">The domain event to add.</param>
    public void AddDomainEvent(IDomainEvent eventItem)
    {
        if (eventItem == null)
            throw new ArgumentNullException(nameof(eventItem));
        DomainEvents.Add(eventItem);
    }

    /// <summary>
    /// Removes a domain event from the aggregate root's event collection.
    /// </summary>
    /// <param name="eventItem">The domain event to remove.</param>
    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        if (eventItem == null)
            throw new ArgumentNullException(nameof(eventItem));
        DomainEvents.Remove(eventItem);
    }
}
