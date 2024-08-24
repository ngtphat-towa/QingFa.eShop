using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities.Attributes
{
    public class ProductAttribute : AuditEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string AttributeCode { get; private set; } = string.Empty;
        public string? Description { get; private set; } = string.Empty;

        public AttributeType Type { get; private set; }
        public bool IsRequired { get; private set; }
        public bool IsFilterable { get; private set; }
        public bool ShowToCustomers { get; private set; }
        public int SortOrder { get; private set; }
        public Guid AttributeGroupId { get; private set; }

        // Navigation properties
        public virtual ProductAttributeGroup AttributeGroup { get; private set; } = default!;
        public virtual ICollection<ProductAttributeOption> AttributeOptions { get; private set; } = new HashSet<ProductAttributeOption>();
        public virtual ICollection<ProductVariantAttribute> VariantAttributes { get; private set; } = new HashSet<ProductVariantAttribute>();

        private ProductAttribute(Guid id, string name, string attributeCode, string? description, AttributeType type, bool isRequired, bool isFilterable, bool showToCustomers, int sortOrder, Guid groupId)
            : base(id)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw CoreException.NullOrEmptyArgument(nameof(name));
            AttributeCode = !string.IsNullOrWhiteSpace(attributeCode) ? attributeCode : throw CoreException.NullOrEmptyArgument(nameof(attributeCode));
            Type = type;
            Description = description;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
            AttributeGroupId = groupId;
        }

        private ProductAttribute() : base(default!)
        {
        }

        public static ProductAttribute Create(
            string name,
            string attributeCode,
            string? description,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder,
            Guid groupId)
        {
            return new ProductAttribute(
                Guid.NewGuid(),
                name,
                attributeCode,
                description,
                type,
                isRequired,
                isFilterable,
                showToCustomers,
                sortOrder,
                groupId);
        }

        public void Update(
            string name,
            string attributeCode,
            string? description,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder,
            string? lastModifiedBy = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(attributeCode)) throw CoreException.NullOrEmptyArgument(nameof(attributeCode));

            Name = name;
            AttributeCode = attributeCode;
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
            Description = description;
            UpdateAuditInfo(lastModifiedBy);
        }

        public void UpdateAttributeGroup(Guid newAttributeGroupId)
        {
            if (newAttributeGroupId == Guid.Empty)
                throw CoreException.InvalidArgument(nameof(newAttributeGroupId), "Attribute group ID must be provided.");

            AttributeGroupId = newAttributeGroupId;
        }

        public void AddOption(ProductAttributeOption option)
        {
            if (option == null) throw CoreException.NullArgument(nameof(option));

            if (AttributeOptions.Any(o => o.OptionValue == option.OptionValue))  // Ensure `OptionValue` uniqueness
            {
                throw new InvalidOperationException("An option with this value already exists.");
            }

            AttributeOptions.Add(option);
        }

        public void RemoveOption(Guid optionId)
        {
            var option = AttributeOptions.SingleOrDefault(o => o.Id == optionId);
            if (option == null)
            {
                throw CoreException.NotFound(nameof(ProductAttributeOption));
            }

            AttributeOptions.Remove(option);
        }

        public void ClearOptions()
        {
            AttributeOptions.Clear();
        }

        public bool IsValidAttributeCode(string attributeCode)
        {
            // Example: attribute code should be alphanumeric and between 3 to 20 characters
            return !string.IsNullOrWhiteSpace(attributeCode) &&
                   attributeCode.Length >= 3 && attributeCode.Length <= 20 &&
                   attributeCode.All(char.IsLetterOrDigit);
        }

        public bool ShouldShowToCustomers()
        {
            return ShowToCustomers;
        }

        public bool IsAttributeRequired()
        {
            return IsRequired;
        }

        public void UpdateDescription(string? description, string? lastModifiedBy = null)
        {
            Description = description;
            UpdateAuditInfo(lastModifiedBy);
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw CoreException.NullOrEmptyArgument(nameof(Name));
            if (string.IsNullOrWhiteSpace(AttributeCode)) throw CoreException.NullOrEmptyArgument(nameof(AttributeCode));
            if (AttributeGroupId == Guid.Empty) throw CoreException.InvalidArgument(nameof(AttributeGroupId), "Attribute group ID must be provided.");
        }

        public enum AttributeType
        {
            Text,
            Number,
            Date,
            Boolean
        }
    }
}
