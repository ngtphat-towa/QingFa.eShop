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

        public static ErrorOr<Brand> Create(
            BrandId id,
            string name,
            string description,
            string logoUrl,
            SeoInfo? seo,
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

            seo = seo ?? SeoInfo.CreateDefault();

            return new Brand(id, name, description, logoUrl, seo, status);
        }

        #endregion

        #region Methods

        public ErrorOr<Brand> UpdateDetails(
            string name,
            string description,
            string logoUrl,
            SeoInfo? seo,
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

            seo = seo ?? SeoInfo.CreateDefault();

            Name = name;
            Description = description;
            LogoUrl = logoUrl;
            Seo = seo;
            Status = status;

            return this;
        }

        public string GetSummary()
        {
            return $"Brand: {Name}, Description: {Description}, LogoUrl: {LogoUrl}, Seo: {Seo.MetaTitle}, Status: {Status}";
        }

        #endregion

        #region Equality Members

        public override bool Equals(object? obj)
        {
            return obj is Brand brand &&
                   EqualityComparer<BrandId>.Default.Equals(Id, brand.Id) &&
                   Name == brand.Name &&
                   Description == brand.Description &&
                   LogoUrl == brand.LogoUrl &&
                   EqualityComparer<SeoInfo>.Default.Equals(Seo, brand.Seo) &&
                   Status == brand.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, LogoUrl, Seo, Status);
        }

        #endregion
    }
}
