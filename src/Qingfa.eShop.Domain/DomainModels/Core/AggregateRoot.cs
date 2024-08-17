using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels.Core
{
    /// <summary>
    /// Represents a base class for aggregate root entities in the domain model,
    /// which includes support for managing domain events inherited from the entity class.
    /// </summary>
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot, IEquatable<AggregateRoot<TId>> where TId : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the aggregate root.</param>
        protected AggregateRoot(TId id) : base(id)
        {
        }

        #region IEquatable<AggregateRoot<TId>> Implementation

        /// <summary>
        /// Determines whether the specified <see cref="AggregateRoot{TId}"/> is equal to the current <see cref="AggregateRoot{TId}"/>.
        /// </summary>
        /// <param name="other">The <see cref="AggregateRoot{TId}"/> to compare with the current <see cref="AggregateRoot{TId}"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="AggregateRoot{TId}"/> is equal to the current <see cref="AggregateRoot{TId}"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(AggregateRoot<TId>? other)
        {
            if (other == null) return false;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="AggregateRoot{TId}"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="AggregateRoot{TId}"/>.</param>
        /// <returns><c>true</c> if the specified object is equal to the current <see cref="AggregateRoot{TId}"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as AggregateRoot<TId>);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current <see cref="AggregateRoot{TId}"/>.</returns>
        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        #endregion
    }
}
