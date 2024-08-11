namespace QingFa.EShop.Domain.DomainModels.Interfaces;

/// <summary>
/// Represents the root of an aggregate in Domain-Driven Design (DDD).
/// An aggregate root is an entity that manages access to a cluster of related objects
/// and ensures the consistency of changes within the aggregate.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public interface IAggregateRoot<TId> : IEntity<TId>
{
}
