﻿using QingFa.EShop.Domain.DomainModels;
using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace Qingfa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a base class for aggregate root entities in the domain model,
    /// which includes support for managing domain events inherited from the entity class.
    /// </summary>
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the aggregate root.</param>
        protected AggregateRoot(TId id) : base(id)
        {
        }
    }
}
