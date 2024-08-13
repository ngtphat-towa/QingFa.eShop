using QingFa.EShop.Domain.Catalogs.ValueObjects.Identities;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Brand : Entity<CatalogBrandId>
    {
        public new CatalogBrandId Id { get; private set; }
        public string Name { get; private set; }
        public string? Bio { get; private set; }
        public string? LogoURL { get; private set; }
        public string? WebsiteURL { get; private set; }
        public string? CountryOfOrigin { get; private set; }
        public int EstablishmentYear { get; private set; }

        // Private constructor to enforce use of the static factory method
        private Brand(CatalogBrandId id, string name, string? description, string? logoURL, string? websiteURL, string? countryOfOrigin, int establishmentYear)
            : base(id)
        {
            Id = id ?? CatalogBrandId.Create(0);
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name cannot be null or empty.", nameof(name)) : name;
            Bio = description;
            LogoURL = logoURL;
            WebsiteURL = websiteURL;
            CountryOfOrigin = countryOfOrigin;
            EstablishmentYear = establishmentYear < 1900 || establishmentYear > DateTime.Now.Year
                ? throw new ArgumentOutOfRangeException(nameof(establishmentYear), "Invalid establishment year.")
                : establishmentYear;
        }

        // Static factory method for creating instances
        public static Brand Create(CatalogBrandId id, string name, string? description, string? logoURL, string? websiteURL, string? countryOfOrigin, int establishmentYear)
        {
            return new Brand(id, name, description, logoURL, websiteURL, countryOfOrigin, establishmentYear);
        }

        // Methods for updating properties
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            Name = name;
        }

        public void UpdateDescription(string? description)
        {
            Bio = description;
        }

        public void UpdateLogoURL(string? logoURL)
        {
            LogoURL = logoURL;
        }

        public void UpdateWebsiteURL(string? websiteURL)
        {
            WebsiteURL = websiteURL;
        }

        public void UpdateCountryOfOrigin(string? countryOfOrigin)
        {
            CountryOfOrigin = countryOfOrigin;
        }

        public void UpdateEstablishmentYear(int establishmentYear)
        {
            if (establishmentYear < 1900 || establishmentYear > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(establishmentYear), "Invalid establishment year.");
            EstablishmentYear = establishmentYear;
        }

        // Protected constructor for EF Core
#pragma warning disable CS8618
        protected Brand()
#pragma warning restore CS8618
        {
        }
    }
}
