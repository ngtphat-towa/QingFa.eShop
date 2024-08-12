using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class CatalogSize : Entity<CatalogSizeId>
    {
        public new CatalogSizeId Id { get; private set; }
        public string SizeLabel { get; private set; }
        public int? MinimumValue { get; private set; }
        public int? MaximumValue { get; private set; }
        public string Unit { get; private set; }

        // Private constructor to enforce use of the factory method
        private CatalogSize(CatalogSizeId catalogSizeId, string sizeLabel, int? minimumValue, int? maximumValue, string unit)
            : base(catalogSizeId)
        {
            Id = catalogSizeId;
            SizeLabel = sizeLabel ?? throw new ArgumentException("Size label cannot be null or empty.", nameof(sizeLabel));
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            Unit = unit ?? throw new ArgumentException("Unit cannot be null or empty.", nameof(unit));
        }

        public static CatalogSize Create(CatalogSizeId catalogSizeId, string sizeLabel, int? minimumValue, int? maximumValue, string unit)
        {
            if (catalogSizeId == null) throw new ArgumentNullException(nameof(catalogSizeId), "CatalogSizeId cannot be null.");
            if (string.IsNullOrWhiteSpace(sizeLabel)) throw new ArgumentException("Size label cannot be null or empty.", nameof(sizeLabel));
            if (minimumValue < 0 || maximumValue < 0) throw new ArgumentException("Minimum and maximum values cannot be negative.", nameof(minimumValue));
            if (minimumValue.HasValue && maximumValue.HasValue && minimumValue > maximumValue) throw new ArgumentException("Minimum value cannot be greater than maximum value.", nameof(minimumValue));
            if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit cannot be null or empty.", nameof(unit));

            return new CatalogSize(catalogSizeId, sizeLabel, minimumValue, maximumValue, unit);
        }

        public void UpdateSizeLabel(string sizeLabel)
        {
            if (string.IsNullOrWhiteSpace(sizeLabel)) throw new ArgumentException("Size label cannot be null or empty.", nameof(sizeLabel));
            SizeLabel = sizeLabel;
        }

        public void UpdateRange(int? minimumValue, int? maximumValue)
        {
            if (minimumValue < 0 || maximumValue < 0) throw new ArgumentException("Minimum and maximum values cannot be negative.", nameof(minimumValue));
            if (minimumValue.HasValue && maximumValue.HasValue && minimumValue > maximumValue) throw new ArgumentException("Minimum value cannot be greater than maximum value.", nameof(minimumValue));
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }

        public void UpdateUnit(string unit)
        {
            if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit cannot be null or empty.", nameof(unit));
            Unit = unit;
        }

        // Protected parameterless constructor for EF Core
#pragma warning disable CS8618
        protected CatalogSize()
#pragma warning restore CS8618
        {
        }
    }
}
