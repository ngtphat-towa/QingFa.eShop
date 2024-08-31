using QingFa.EShop.Domain.Core.ValueObjects;

namespace QingFa.EShop.Domain.Common.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        public static readonly Address Default = new Address("123 Default St", "Default City", "DC", "00000", "Default Country");
        public static readonly Address Empty = new Address(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        private Address(string street, string city, string state, string postalCode, string country)
        {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        public static Address Create(string street, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentException("Street cannot be null or whitespace.", nameof(street));
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City cannot be null or whitespace.", nameof(city));
            }
            if (string.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentException("State cannot be null or whitespace.", nameof(state));
            }
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentException("PostalCode cannot be null or whitespace.", nameof(postalCode));
            }
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentException("Country cannot be null or whitespace.", nameof(country));
            }

            return new Address(street, city, state, postalCode, country);
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {PostalCode}, {Country}";
        }

        public static Address FromString(string addressStr)
        {
            var parts = addressStr.Split(',');
            if (parts.Length != 5)
            {
                throw new ArgumentException("Invalid address format.");
            }

            return new Address(parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), parts[3].Trim(), parts[4].Trim());
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return PostalCode;
            yield return Country;
        }
    }
}
