namespace QingFa.EShop.Domain.Core.ValueObjects
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected static bool EqualOperator(ValueObject? left, ValueObject? right)
        {
            if (left is null || right is null)
            {
                return ReferenceEquals(left, right);
            }

            return left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<object?> GetEqualityComponents();

        public bool Equals(ValueObject? other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && Equals(other);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(0, (hashCode, component) => HashCode.Combine(hashCode, component));
        }
    }
}
