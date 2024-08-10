using System;
using System.Collections.Generic;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents the root of an aggregate in Domain-Driven Design (DDD).
    /// An aggregate root is an entity that manages access to a cluster of related objects
    /// and ensures the consistency of changes within the aggregate.
    /// </summary>
    public interface IAggregateRoot : IEntity
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
    }
}
