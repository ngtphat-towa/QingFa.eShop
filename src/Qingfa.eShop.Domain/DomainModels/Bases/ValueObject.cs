using System.Reflection;

namespace QingFa.EShop.Domain.DomainModels.Bases;

/// <summary>
/// Represents a base class for value objects in the domain model.
/// Value objects are immutable types that are defined by their values
/// rather than their identities. Equality is based on the values of 
/// their components.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Compares two value objects for equality based on their values.
    /// </summary>
    /// <param name="left">The first value object to compare.</param>
    /// <param name="right">The second value object to compare.</param>
    /// <returns>
    /// <c>true</c> if the value objects are equal; otherwise, <c>false</c>.
    /// </returns>
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left?.Equals(right) != false;
    }

    /// <summary>
    /// Compares two value objects for inequality based on their values.
    /// </summary>
    /// <param name="left">The first value object to compare.</param>
    /// <param name="right">The second value object to compare.</param>
    /// <returns>
    /// <c>true</c> if the value objects are not equal; otherwise, <c>false</c>.
    /// </returns>
    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    /// <summary>
    /// Gets the components that are used to determine equality for this value object.
    /// </summary>
    /// <returns>
    /// An enumeration of objects that represent the components of this value object.
    /// </returns>
    protected virtual IEnumerable<object?> GetEqualityComponents()
    {
        // Use reflection to get all public instance properties
        return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(prop => prop.GetValue(this));
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
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
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
