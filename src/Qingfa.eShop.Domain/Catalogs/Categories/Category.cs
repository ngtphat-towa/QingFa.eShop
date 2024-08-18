using ErrorOr;

using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Categories
{
    public class Category : AggregateRoot<CategoryId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string BannerURL { get; private set; }
        public CategoryStatus Status { get; private set; }
        public bool IncludeToStore { get; private set; }
        public CategoryId? ParentId { get; private set; }
        public SeoInfo Seo { get; private set; }

        // Fully parameterized constructor - protected to restrict access
        protected Category(
            CategoryId id,
            string name,
            string description,
            string bannerURL,
            CategoryStatus status,
            bool includeToStore,
            CategoryId? parentId,
            SeoInfo seo
        ) : base(id)
        {
            Name = name;
            Description = description;
            BannerURL = bannerURL;
            Status = status;
            IncludeToStore = includeToStore;
            ParentId = parentId;
            Seo = seo;
        }

        // Parameterless constructor for EF Core
#pragma warning disable CS8618
        protected Category() : base(default!)
#pragma warning restore CS8618
        {
        }

        /// <summary>
        /// Factory method to create a Category with error handling.
        /// </summary>
        public static ErrorOr<Category> Create(
            CategoryId id,
            string name,
            string description,
            string bannerURL,
            CategoryStatus status,
            bool includeToStore,
            CategoryId? parentId,
            SeoInfo? seo
        )
        {
            var validationErrors = ValidateCategoryDetails(name, description, bannerURL, seo);
            if (validationErrors.Count > 0)
            {
                return validationErrors;
            }
            seo = seo ?? SeoInfo.CreateDefault();

            var category = new Category(
                id,
                name,
                description,
                bannerURL,
                status,
                includeToStore,
                parentId,
                seo
            );

            return category;
        }

        /// <summary>
        /// Method to update the Category details.
        /// </summary>
        public ErrorOr<Category> UpdateDetails(
            string name,
            string description,
            string bannerURL,
            CategoryStatus status,
            SeoInfo seo
        )
        {
            var validationErrors = ValidateCategoryDetails(name, description, bannerURL, seo);
            if (validationErrors.Count > 0)
            {
                return validationErrors;
            }
            seo = seo ?? SeoInfo.CreateDefault();

            Name = name;
            Description = description;
            BannerURL = bannerURL;
            Status = status;
            Seo = seo;

            return this;
        }

        /// <summary>
        /// Method to toggle the active status of the Category.
        /// </summary>
        public void ToggleActiveStatus()
        {
            Status = Status == CategoryStatus.Active ? CategoryStatus.Inactive : CategoryStatus.Active;
        }

        /// <summary>
        /// Method to update the ParentId.
        /// </summary>
        public ErrorOr<Category> UpdateParent(CategoryId? newParentId)
        {
            if (ParentId == newParentId)
            {
                return CoreErrors.ValidationError(nameof(ParentId), "New ParentId must be different from the current one.");
            }

            ParentId = newParentId;
            return this;
        }

        /// <summary>
        /// Method to provide a summary of the Category.
        /// </summary>
        public string GetSummary()
        {
            return $"Category: {Name}, Status: {Status}, ParentId: {ParentId}, Seo: {Seo.MetaTitle}";
        }

        #region Private Methods

        private static List<Error> ValidateCategoryDetails(string name, string description, string bannerURL, SeoInfo? seo)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(CoreErrors.ValidationError(nameof(Name), "Name cannot be empty."));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                errors.Add(CoreErrors.ValidationError(nameof(Description), "Description cannot be empty."));
            }

            if (string.IsNullOrWhiteSpace(bannerURL))
            {
                errors.Add(CoreErrors.ValidationError(nameof(BannerURL), "BannerURL cannot be empty."));
            }

            return errors;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Category other)
                return false;

            return Id.Equals(other.Id) &&
                   Name == other.Name &&
                   Description == other.Description &&
                   BannerURL == other.BannerURL &&
                   (Seo?.Equals(other.Seo) ?? other.Seo == null) &&
                   Status == other.Status &&
                   IncludeToStore == other.IncludeToStore &&
                   (ParentId?.Equals(other.ParentId) ?? other.ParentId == null);
        }



        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, BannerURL, Seo, Status, IncludeToStore, ParentId);
        }

        #endregion
    }
}
