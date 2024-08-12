using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class CatalogColor : Entity<CatalogColorId>
    {
        public string BaseColor { get; private set; }
        public string DescriptiveColorName { get; private set; }
        public string HexCode { get; private set; }
        public string RgbCode { get; private set; }

        private CatalogColor(CatalogColorId catalogColorId, string baseColor, string descriptiveColorName, string hexCode, string rgbCode)
            : base(catalogColorId)
        {
            BaseColor = baseColor ?? throw CoreException.NullArgument(nameof(baseColor));
            DescriptiveColorName = descriptiveColorName ?? throw CoreException.NullArgument(nameof(descriptiveColorName));
            HexCode = hexCode ?? throw CoreException.NullArgument(nameof(hexCode));
            RgbCode = rgbCode ?? throw CoreException.NullArgument(nameof(rgbCode));
        }

        public static CatalogColor Create(CatalogColorId catalogColorId, string baseColor, string descriptiveColorName, string hexCode, string rgbCode)
        {
            if (string.IsNullOrWhiteSpace(baseColor)) throw CoreException.NullArgument(nameof(baseColor));
            if (string.IsNullOrWhiteSpace(descriptiveColorName)) throw CoreException.NullArgument(nameof(descriptiveColorName));
            if (string.IsNullOrWhiteSpace(hexCode)) throw CoreException.NullArgument(nameof(hexCode));
            if (string.IsNullOrWhiteSpace(rgbCode)) throw CoreException.NullArgument(nameof(rgbCode));
            return new CatalogColor(catalogColorId, baseColor, descriptiveColorName, hexCode, rgbCode);
        }

        public void UpdateDescriptiveColorName(string descriptiveColorName)
        {
            if (string.IsNullOrWhiteSpace(descriptiveColorName)) throw CoreException.NullArgument(nameof(descriptiveColorName));
            DescriptiveColorName = descriptiveColorName;
        }

        public void UpdateHexCode(string hexCode)
        {
            if (string.IsNullOrWhiteSpace(hexCode)) throw CoreException.NullArgument(nameof(hexCode));
            HexCode = hexCode;
        }

        public void UpdateRgbCode(string rgbCode)
        {
            if (string.IsNullOrWhiteSpace(rgbCode)) throw CoreException.NullArgument(nameof(rgbCode));
            RgbCode = rgbCode;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected CatalogColor()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }
    }

    public sealed record CatalogColorId
    {
        public int Value { get; }

        private CatalogColorId(int value)
        {
            if (value <= 0) throw CoreException.InvalidArgument(nameof(value));
            Value = value;
        }

        public static CatalogColorId Create(int value)
        {
            return new CatalogColorId(value);
        }
    }
}
