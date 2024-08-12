namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public record Price
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        // Private constructor for internal use
        private Price(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));

            Amount = amount;
            Currency = currency;
        }

        // Static factory method to create a new Price instance
        public static Price Create(decimal amount, string currency)
        {
            return new Price(amount, currency);
        }

        // Default constructor for EF Core
#pragma warning disable CS8618 
        private Price() { }
#pragma warning restore CS8618 

        public Price ApplyDiscount(decimal discountAmount)
        {
            if (discountAmount < 0) throw new ArgumentException("Discount amount cannot be negative.", nameof(discountAmount));
            return new Price(Amount - discountAmount, Currency);
        }

        public Price ApplyPercentageDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100) throw new ArgumentException("Percentage must be between 0 and 100.", nameof(percentage));
            var discountAmount = Amount * (percentage / 100);
            return new Price(Amount - discountAmount, Currency);
        }

        public Price AddTax(decimal taxRate)
        {
            if (taxRate < 0) throw new ArgumentException("Tax rate cannot be negative.", nameof(taxRate));
            var taxAmount = Amount * (taxRate / 100);
            return new Price(Amount + taxAmount, Currency);
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}
