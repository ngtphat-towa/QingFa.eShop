using QingFa.EShop.Domain.Catalogs.ValueObjects.Identities;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class SizeOption : Entity<SizeOptionId>
    {
        public string SizeLabel { get; private set; }
        public int? MinimumValue { get; private set; }
        public int? MaximumValue { get; private set; }
        public string Unit { get; private set; }
        public int InventoryCount { get; private set; }
        public bool IsAvailable => InventoryCount > 0;

        // Private constructor to enforce use of the factory method
        private SizeOption(SizeOptionId id, string sizeLabel, int? minimumValue, int? maximumValue, string unit, int inventoryCount)
            : base(id)
        {
            SizeLabel = ValidateSizeLabel(sizeLabel);
            MinimumValue = ValidateRange(minimumValue, maximumValue);
            MaximumValue = ValidateRange(maximumValue, minimumValue);
            Unit = ValidateUnit(unit);
            InventoryCount = ValidateInventoryCount(inventoryCount);
        }

        public static SizeOption Create(SizeOptionId id, string sizeLabel, int? minimumValue, int? maximumValue, string unit, int inventoryCount)
        {
            if (id == null) throw CoreExceptionFactory.CreateNullArgumentException(nameof(id), "StyleOptionId cannot be null.");
            return new SizeOption(id, sizeLabel, minimumValue, maximumValue, unit, inventoryCount);
        }

        public void UpdateSizeLabel(string sizeLabel)
        {
            SizeLabel = ValidateSizeLabel(sizeLabel);
        }

        public void UpdateRange(int? minimumValue, int? maximumValue)
        {
            MinimumValue = ValidateRange(minimumValue, maximumValue);
            MaximumValue = ValidateRange(maximumValue, minimumValue);
        }

        public void UpdateUnit(string unit)
        {
            Unit = ValidateUnit(unit);
        }

        public void UpdateInventoryCount(int inventoryCount)
        {
            InventoryCount = ValidateInventoryCount(inventoryCount);
        }

        private static string ValidateSizeLabel(string sizeLabel)
        {
            if (string.IsNullOrWhiteSpace(sizeLabel))
                throw new ArgumentException("Size label cannot be null or empty.", nameof(sizeLabel));
            return sizeLabel;
        }

        private static int? ValidateRange(int? value, int? otherValue)
        {
            if (value.HasValue && value < 0)
                throw new ArgumentException("Value cannot be negative.", nameof(value));
            if (otherValue.HasValue && value.HasValue && value > otherValue)
                throw new ArgumentException("Value cannot be greater than the other value.", nameof(value));
            return value;
        }

        private static string ValidateUnit(string unit)
        {
            if (string.IsNullOrWhiteSpace(unit))
                throw new ArgumentException("Unit cannot be null or empty.", nameof(unit));
            return unit;
        }

        private static int ValidateInventoryCount(int inventoryCount)
        {
            if (inventoryCount < 0)
                throw new ArgumentException("Inventory count cannot be negative.", nameof(inventoryCount));
            return inventoryCount;
        }

        // Protected parameterless constructor for EF Core
#pragma warning disable CS8618
        protected SizeOption()
#pragma warning restore CS8618
        {
        }
    }
}
