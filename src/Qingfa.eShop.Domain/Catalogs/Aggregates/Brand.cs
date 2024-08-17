using ErrorOr;

using QingFa.EShop.Domain.Catalogs.ObjectValues;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Aggregates
{
    public class Brand : AggregateRoot<BrandId>
    {
        // Properties
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string LogoUrl { get; private set; }
        public SeoInfo Seo { get; private set; }

        // Fully parameterized constructor - protected to restrict access
        protected Brand(
            BrandId id,
            string name,
            string description,
            string logoUrl,
            SeoInfo seo
        ) : base(id)
        {
            Name = name;
            Description = description;
            LogoUrl = logoUrl;
            Seo = seo;
        }

        // Parameterless constructor for EF Core
#pragma warning disable CS8618
        protected Brand() : base(default!)
#pragma warning restore CS8618
        {
        }

        // Factory method to create a Brand with error handling
        public static ErrorOr<Brand> Create(
            BrandId id,
            string name,
            string description,
            string logoUrl,
            SeoInfo seo
        )
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return CoreErrors.ValidationError(nameof(name), "Name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return CoreErrors.ValidationError(nameof(description), "Description cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(logoUrl))
            {
                return CoreErrors.ValidationError(nameof(logoUrl), "LogoUrl cannot be empty.");
            }

            if (seo == null)
            {
                return CoreErrors.ValidationError(nameof(seo), "SeoInfo cannot be null.");
            }

            Brand brand = new(
                id,
                name,
                description,
                logoUrl,
                seo
            );

            return brand;
        }

        // Method to update the Brand details
        public ErrorOr<Brand> UpdateDetails(
            string name,
            string description,
            string logoUrl,
            SeoInfo seo
        )
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return CoreErrors.ValidationError(nameof(name), "Name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return CoreErrors.ValidationError(nameof(description), "Description cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(logoUrl))
            {
                return CoreErrors.ValidationError(nameof(logoUrl), "LogoUrl cannot be empty.");
            }

            if (seo == null)
            {
                return CoreErrors.ValidationError(nameof(seo), "SeoInfo cannot be null.");
            }

            Name = name;
            Description = description;
            LogoUrl = logoUrl;
            Seo = seo;

            return this;
        }

        // Method to provide a summary of the Brand
        public string GetSummary()
        {
            return $"Brand: {Name}, Description: {Description}, LogoUrl: {LogoUrl}, Seo: {Seo.MetaTitle}";
        }
    }
}
