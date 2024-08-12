using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

public class CatalogBrand : Entity<CatalogBrandId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string LogoURL { get; private set; }
    public string WebsiteURL { get; private set; }
    public string CountryOfOrigin { get; private set; }
    public int EstablishmentYear { get; private set; }

    private CatalogBrand(CatalogBrandId catalogBrandId, string name, string description, string logoURL, string websiteURL, string countryOfOrigin, int establishmentYear)
        : base(catalogBrandId)
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name cannot be null or empty.", nameof(name)) : name;
        Description = description;
        LogoURL = logoURL;
        WebsiteURL = websiteURL;
        CountryOfOrigin = string.IsNullOrWhiteSpace(countryOfOrigin) ? throw new ArgumentException("Country of Origin cannot be null or empty.", nameof(countryOfOrigin)) : countryOfOrigin;
        EstablishmentYear = (establishmentYear < 1900 || establishmentYear > DateTime.Now.Year) ? throw new ArgumentOutOfRangeException(nameof(establishmentYear), "Invalid establishment year.") : establishmentYear;
    }

    public static CatalogBrand Create(CatalogBrandId catalogBrandId, string name, string description, string logoURL, string websiteURL, string countryOfOrigin, int establishmentYear)
    {
        return new CatalogBrand(catalogBrandId, name, description, logoURL, websiteURL, countryOfOrigin, establishmentYear);
    }

    public void UpdateDescription(string description)
    {
        Description = string.IsNullOrWhiteSpace(description) ? throw new ArgumentException("Description cannot be null or empty.", nameof(description)) : description;
    }

    public void UpdateLogoURL(string logoURL) => LogoURL = logoURL;
    public void UpdateWebsiteURL(string websiteURL) => WebsiteURL = websiteURL;

    public void UpdateCountryOfOrigin(string countryOfOrigin)
    {
        CountryOfOrigin = string.IsNullOrWhiteSpace(countryOfOrigin) ? throw new ArgumentException("Country of Origin cannot be null or empty.", nameof(countryOfOrigin)) : countryOfOrigin;
    }

    public void UpdateEstablishmentYear(int establishmentYear)
    {
        if (establishmentYear < 1900 || establishmentYear > DateTime.Now.Year)
            throw new ArgumentOutOfRangeException(nameof(establishmentYear), "Invalid establishment year.");
        EstablishmentYear = establishmentYear;
    }

    // Parameterless constructor for ORM
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected CatalogBrand() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
