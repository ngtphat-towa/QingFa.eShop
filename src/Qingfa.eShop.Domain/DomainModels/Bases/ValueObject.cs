namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a base class for value objects in the domain model.
    /// Value objects are immutable types that are defined by their values
    /// rather than their identities. Equality is based on the values of 
    /// their components.
    /// </summary>
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// Gets the components that are used to determine equality for this value object.
        /// </summary>
        /// <returns>
        /// An enumeration of objects that represent the components of this value object.
        /// </returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(ValueObject? other)
        {
            if (other is null)
            {
                return false;
            }

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is ValueObject other)
            {
                return Equals(other);
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for the value object.
        /// </summary>
        /// <returns>
        /// A hash code for the current object, based on its equality components.
        /// </returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
