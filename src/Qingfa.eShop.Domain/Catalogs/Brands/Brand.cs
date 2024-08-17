using ErrorOr;

using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Brands
{
    public class Brand : AggregateRoot<BrandId>
    {
        #region Properties

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string LogoUrl { get; private set; }
        public SeoInfo Seo { get; private set; }
        public BrandStatus Status { get; private set; }

        #endregion

        #region Constructors

        // Fully parameterized constructor - protected to restrict access
        protected Brand(
            BrandId id,
            string name,
            string description,
            string logoUrl,
            SeoInfo seo,
            BrandStatus status
        ) : base(id)
        {
            Name = name;
            Description = description;
            LogoUrl = logoUrl;
            Seo = seo;
            Status = status;
        }

        // Parameterless constructor for EF Core
#pragma warning disable CS8618
        protected Brand() : base(default!)
#pragma warning restore CS8618
        {
        }

        #endregion

        #region Factory Methods

        // Factory method to create a Brand with error handling
        public static ErrorOr<Brand> Create(
            BrandId id,
            string name,
            string description,
            string logoUrl,
            SeoInfo seo,
            BrandStatus status
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

            // Create the brand with the specified status
            Brand brand = new(
                id,
                name,
                description,
                logoUrl,
                seo,
                status
            );

            return brand;
        }

        #endregion

        #region Methods

        // Method to update the Brand details
        public ErrorOr<Brand> UpdateDetails(
            string name,
            string description,
            string logoUrl,
            SeoInfo seo,
            BrandStatus status
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
            Status = status;

            return this;
        }

        // Method to provide a summary of the Brand
        public string GetSummary()
        {
            return $"Brand: {Name}, Description: {Description}, LogoUrl: {LogoUrl}, Seo: {Seo.MetaTitle}, Status: {Status}";
        }

        #endregion
    }
}
