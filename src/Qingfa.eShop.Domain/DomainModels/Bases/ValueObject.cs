namespace QingFa.EShop.Domain.DomainModels
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public static bool operator ==(ValueObject? a, ValueObject? b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject? a, ValueObject? b) =>
            !(a == b);

        public bool Equals(ValueObject? other) =>
            other is not null && ValuesAreEqual(other);

        public override bool Equals(object? obj) =>
            obj is ValueObject valueObject && ValuesAreEqual(valueObject);

        public override int GetHashCode()
        {
            // Initialize hash code
            var hashCode = 17;

            // Combine hash codes of atomic values
            foreach (var value in GetAtomicValues())
            {
                hashCode = HashCode.Combine(hashCode, value);
            }

            return hashCode;
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        private bool ValuesAreEqual(ValueObject valueObject) =>
            GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }
}
