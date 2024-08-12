using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class CatalogSize : Entity<CatalogSizeId>
    {
        public string SizeLabel { get; private set; }    // e.g., "Small", "Medium"
        public int? MinimumValue { get; private set; }   // Nullable for size categories
        public int? MaximumValue { get; private set; }   // Nullable for size categories
        public string Unit { get; private set; }         // e.g., "cm", "inches"

        private CatalogSize(CatalogSizeId sizeId, string sizeLabel, int? minimumValue, int? maximumValue, string unit)
            : base(sizeId)
        {
            SizeLabel = sizeLabel ?? throw CoreException.NullArgument(nameof(sizeLabel));
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            Unit = unit ?? throw CoreException.NullArgument(nameof(unit));
        }

        public static CatalogSize Create(CatalogSizeId sizeId, string sizeLabel, int? minimumValue, int? maximumValue, string unit)
        {
            if (string.IsNullOrWhiteSpace(sizeLabel)) throw CoreException.NullArgument(nameof(sizeLabel));
            if (minimumValue < 0 || maximumValue < 0) throw CoreException.InvalidArgument(nameof(minimumValue));
            if (minimumValue.HasValue && maximumValue.HasValue && minimumValue > maximumValue) throw CoreException.InvalidArgument(nameof(minimumValue));
            if (string.IsNullOrWhiteSpace(unit)) throw CoreException.NullArgument(nameof(unit));
            return new CatalogSize(sizeId, sizeLabel, minimumValue, maximumValue, unit);
        }

        public void UpdateSizeLabel(string sizeLabel)
        {
            if (string.IsNullOrWhiteSpace(sizeLabel)) throw CoreException.NullArgument(nameof(sizeLabel));
            SizeLabel = sizeLabel;
        }

        public void UpdateRange(int? minimumValue, int? maximumValue)
        {
            if (minimumValue < 0 || maximumValue < 0) throw CoreException.InvalidArgument(nameof(minimumValue));
            if (minimumValue.HasValue && maximumValue.HasValue && minimumValue > maximumValue) throw CoreException.InvalidArgument(nameof(minimumValue));
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }

        public void UpdateUnit(string unit)
        {
            if (string.IsNullOrWhiteSpace(unit)) throw CoreException.NullArgument(nameof(unit));
            Unit = unit;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected CatalogSize()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
