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
            public CategoryStatus Status { get; private set; }  // Updated property
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

            // Factory method to create a Category with error handling
            public static ErrorOr<Category> Create(
                CategoryId id,
                string name,
                string description,
                string bannerURL,
                CategoryStatus status,  // Updated parameter
                bool includeToStore,
                CategoryId? parentId,
                SeoInfo seo
            )
            {
                var validationErrors = ValidateCategoryDetails(name, description, bannerURL, seo);
                if (validationErrors.Count > 0)
                {
                    return validationErrors;
                }

                var category = new Category(
                    id,
                    name,
                    description,
                    bannerURL,
                    status,  // Updated parameter
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
                CategoryStatus status,  // Updated parameter
                SeoInfo seo
            )
            {
                var validationErrors = ValidateCategoryDetails(name, description, bannerURL, seo);
                if (validationErrors.Count > 0)
                {
                    return validationErrors;
                }

                Name = name;
                Description = description;
                BannerURL = bannerURL;
                Status = status;  // Updated property
                Seo = seo;

                return this;
            }

            // Method to toggle the active status of the Category
            public void ToggleActiveStatus()
            {
                Status = Status == CategoryStatus.Active ? CategoryStatus.Inactive : CategoryStatus.Active;
            }

            // Method to update the ParentId
            public ErrorOr<Category> UpdateParent(CategoryId? newParentId)
            {
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

            #region Private Methods

            private static List<Error> ValidateCategoryDetails(string name, string description, string bannerURL, SeoInfo seo)
            {
                var errors = new List<Error>();

                if (string.IsNullOrWhiteSpace(name))
                {
                    errors.Add(CoreErrors.ValidationError(nameof(name), "Name cannot be empty."));
                }

                if (string.IsNullOrWhiteSpace(description))
                {
                    errors.Add(CoreErrors.ValidationError(nameof(description), "Description cannot be empty."));
                }

                if (string.IsNullOrWhiteSpace(bannerURL))
                {
                    errors.Add(CoreErrors.ValidationError(nameof(bannerURL), "BannerURL cannot be empty."));
                }

                if (seo == null)
                {
                    errors.Add(CoreErrors.ValidationError(nameof(seo), "SeoInfo cannot be null."));
                }

                return errors;
            }

            #endregion
        }
    }
