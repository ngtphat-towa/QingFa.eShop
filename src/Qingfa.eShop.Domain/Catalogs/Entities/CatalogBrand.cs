using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities;

/// <summary>
/// Represents a brand in the catalog.
/// </summary>
public class CatalogBrand : Entity<CatalogBrandId>
{
    /// <summary>
    /// Gets the name of the brand.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the brand.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the URL of the brand's logo.
    /// </summary>
    public string LogoURL { get; private set; }

    /// <summary>
    /// Gets the URL of the brand's website.
    /// </summary>
    public string WebsiteURL { get; private set; }

    /// <summary>
    /// Gets the country where the brand originates.
    /// </summary>
    public string CountryOfOrigin { get; private set; }

    /// <summary>
    /// Gets the year the brand was established.
    /// </summary>
    public int EstablishmentYear { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CatalogBrand"/> class.
    /// </summary>
    /// <param name="catalogBrandId">The unique identifier for the catalog brand.</param>
    /// <param name="name">The name of the brand.</param>
    /// <param name="description">The description of the brand.</param>
    /// <param name="logoURL">The URL of the brand's logo.</param>
    /// <param name="websiteURL">The URL of the brand's website.</param>
    /// <param name="countryOfOrigin">The country where the brand originates.</param>
    /// <param name="establishmentYear">The year the brand was established.</param>
    private CatalogBrand(CatalogBrandId catalogBrandId, string name, string description, string logoURL, string websiteURL, string countryOfOrigin, int establishmentYear)
        : base(catalogBrandId)
    {
        Name = name ?? throw CoreException.NullOrEmptyArgument(nameof(name));
        Description = description;
        LogoURL = logoURL;
        WebsiteURL = websiteURL;
        CountryOfOrigin = countryOfOrigin ?? throw CoreException.NullOrEmptyArgument(nameof(countryOfOrigin));
        EstablishmentYear = establishmentYear;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="CatalogBrand"/> class.
    /// </summary>
    /// <param name="catalogBrandId">The unique identifier for the catalog brand.</param>
    /// <param name="name">The name of the brand.</param>
    /// <param name="description">The description of the brand.</param>
    /// <param name="logoURL">The URL of the brand's logo.</param>
    /// <param name="websiteURL">The URL of the brand's website.</param>
    /// <param name="countryOfOrigin">The country where the brand originates.</param>
    /// <param name="establishmentYear">The year the brand was established.</param>
    /// <returns>A new instance of the <see cref="CatalogBrand"/> class.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="establishmentYear"/> is outside the valid range.</exception>
    public static CatalogBrand Create(CatalogBrandId catalogBrandId, string name, string description, string logoURL, string websiteURL, string countryOfOrigin, int establishmentYear)
    {
        if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument( nameof(name));
        if (string.IsNullOrWhiteSpace(countryOfOrigin)) throw CoreException.NullOrEmptyArgument(nameof(countryOfOrigin));
        if (establishmentYear < 1900 || establishmentYear > DateTime.Now.Year) throw new ArgumentOutOfRangeException(nameof(establishmentYear), "Invalid establishment year.");

        return new CatalogBrand(catalogBrandId, name, description, logoURL, websiteURL, countryOfOrigin, establishmentYear);
    }

    /// <summary>
    /// Updates the description of the brand.
    /// </summary>
    /// <param name="description">The new description of the brand.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="description"/> is null or empty.</exception>
    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));
        Description = description;
    }

    /// <summary>
    /// Updates the URL of the brand's logo.
    /// </summary>
    /// <param name="logoURL">The new URL of the brand's logo.</param>
    public void UpdateLogoURL(string logoURL)
    {
        LogoURL = logoURL;
    }

    /// <summary>
    /// Updates the URL of the brand's website.
    /// </summary>
    /// <param name="websiteURL">The new URL of the brand's website.</param>
    public void UpdateWebsiteURL(string websiteURL)
    {
        WebsiteURL = websiteURL;
    }

    /// <summary>
    /// Updates the country of origin for the brand.
    /// </summary>
    /// <param name="countryOfOrigin">The new country of origin.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="countryOfOrigin"/> is null or empty.</exception>
    public void UpdateCountryOfOrigin(string countryOfOrigin)
    {
        if (string.IsNullOrWhiteSpace(countryOfOrigin)) throw CoreException.NullOrEmptyArgument(nameof(countryOfOrigin));
        CountryOfOrigin = countryOfOrigin;
    }

    /// <summary>
    /// Updates the year the brand was established.
    /// </summary>
    /// <param name="establishmentYear">The new establishment year.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="establishmentYear"/> is outside the valid range.</exception>
    public void UpdateEstablishmentYear(int establishmentYear)
    {
        if (establishmentYear < 1900 || establishmentYear > DateTime.Now.Year) throw CoreException.NullOrEmptyArgument(nameof(establishmentYear));
        EstablishmentYear = establishmentYear;
    }
}
