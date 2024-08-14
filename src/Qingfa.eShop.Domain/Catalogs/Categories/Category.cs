using ErrorOr;

using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Brands
{
    public class Category : Entity<CategoryId>
    {
        public string TypeName { get; private set; }
        public string Bio { get; private set; }
        public string ImageUrl { get; private set; }
        public bool IsActive { get; private set; }
        public CategoryId? ParentId { get; private set; }

        // Fully parameterized constructor - protected to restrict access
        protected Category(
            CategoryId id,
            string typeName,
            string bio,
            string imageUrl,
            bool isActive,
            CategoryId? parentId
        ) : base(id)
        {
            TypeName = typeName;
            Bio = bio;
            ImageUrl = imageUrl;
            IsActive = isActive;
            ParentId = parentId;
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
            string typeName,
            string bio,
            string imageUrl,
            bool isActive,
            CategoryId? parentId
        )
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return CoreErrors.ValidationError(nameof(typeName), "TypeName cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(bio))
            {
                return CoreErrors.ValidationError(nameof(bio), "Bio cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return CoreErrors.ValidationError(nameof(imageUrl), "ImageUrl cannot be empty.");
            }

            Category category = new(
                id,
                typeName,
                bio,
                imageUrl,
                isActive,
                parentId
            );

            return category;
        }

        // Method to update the Category details
        public ErrorOr<Category> UpdateDetails(
            string typeName,
            string bio,
            string imageUrl,
            bool isActive
        )
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return CoreErrors.ValidationError(nameof(typeName), "TypeName cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(bio))
            {
                return CoreErrors.ValidationError(nameof(bio), "Bio cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return CoreErrors.ValidationError(nameof(imageUrl), "ImageUrl cannot be empty.");
            }

            TypeName = typeName;
            Bio = bio;
            ImageUrl = imageUrl;
            IsActive = isActive;

            return this;
        }

        // Method to toggle the active status of the Category
        public void ToggleActiveStatus()
        {
            IsActive = !IsActive;
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
            return $"Category: {TypeName}, Active: {IsActive}, ParentId: {ParentId}";
        }
    }
}
