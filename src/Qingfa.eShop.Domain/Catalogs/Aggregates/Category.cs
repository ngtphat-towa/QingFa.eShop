using ErrorOr;

using QingFa.EShop.Domain.Catalogs.ObjectValues;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Aggregates
{
    public class Category : AggregateRoot<CategoryId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string BannerURL { get; private set; }
        public EntityStatus Status { get; private set; }
        public bool IncludeToStore { get; private set; }
        public CategoryId? ParentId { get; private set; }
        public SeoInfo Seo { get; private set; } // Add this property

        // Fully parameterized constructor - protected to restrict access
        protected Category(
            CategoryId id,
            string name,
            string description,
            string bannerURL,
            EntityStatus status,
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

        // Factory method to create a Category with error handling
        public static ErrorOr<Category> Create(
            CategoryId id,
            string name,
            string description,
            string bannerURL,
            EntityStatus status,
            bool includeToStore,
            CategoryId? parentId,
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

            if (string.IsNullOrWhiteSpace(bannerURL))
            {
                return CoreErrors.ValidationError(nameof(bannerURL), "BannerURL cannot be empty.");
            }

            if (seo == null)
            {
                return CoreErrors.ValidationError(nameof(seo), "SeoInfo cannot be null.");
            }

            Category category = new(
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

        // Method to update the Category details
        public ErrorOr<Category> UpdateDetails(
            string name,
            string description,
            string bannerURL,
            EntityStatus status,
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

            if (string.IsNullOrWhiteSpace(bannerURL))
            {
                return CoreErrors.ValidationError(nameof(bannerURL), "BannerURL cannot be empty.");
            }

            if (seo == null)
            {
                return CoreErrors.ValidationError(nameof(seo), "SeoInfo cannot be null.");
            }

            Name = name;
            Description = description;
            BannerURL = bannerURL;
            Status = status;
            Seo = seo;

            return this;
        }

        // Method to toggle the active status of the Category
        public void ToggleActiveStatus()
        {
            Status = Status == EntityStatus.Active ? EntityStatus.Inactive : EntityStatus.Active;
        }

        // Method to update the ParentId
        public ErrorOr<Category> UpdateParent(CategoryId? newParentId)
        {
            // Example validation: Ensure ParentId is not the same as the current one
            if (ParentId == newParentId)
            {
                return CoreErrors.ValidationError(nameof(ParentId), "New ParentId must be different from the current one.");
            }

            ParentId = newParentId;
            return this;
        }

        // Method to provide a summary of the Category
        public string GetSummary()
        {
            return $"Category: {Name}, Status: {Status}, ParentId: {ParentId}, Seo: {Seo.MetaTitle}";
        }
    }
}
