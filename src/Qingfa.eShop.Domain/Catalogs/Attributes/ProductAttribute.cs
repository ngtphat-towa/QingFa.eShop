using ErrorOr;

using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Attributes
{
    /// <summary>
    /// Represents a product attribute in the catalog.
    /// </summary>
    public class ProductAttribute : Entity<ProductAttributeId>
    {
        #region Properties

        public string Name { get; private set; }
        public string AttributeCode { get; private set; }
        public AttributeType Type { get; private set; }
        public bool IsRequired { get; private set; }
        public bool IsFilterable { get; private set; }
        public bool ShowToCustomers { get; private set; }
        public int SortOrder { get; private set; }
        public AttributeGroupId AttributeGroupId { get; private set; }

        private readonly List<AttributeOption> _options = new();
        public IReadOnlyList<AttributeOption> Options => _options.AsReadOnly();

        #endregion

        #region Constructors

        protected ProductAttribute(
            ProductAttributeId id,
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder,
            AttributeGroupId attributeGroupId
        ) : base(id)
        {
            Name = name;
            AttributeCode = attributeCode;
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
            AttributeGroupId = attributeGroupId;
        }

#pragma warning disable CS8618 
        protected ProductAttribute() : base(default!) { }
#pragma warning restore CS8618 

        #endregion

        #region Factory Methods

        public static ErrorOr<ProductAttribute> Create(
            ProductAttributeId id,
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder,
            AttributeGroupId attributeGroupId
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                return CoreErrors.ValidationError(nameof(name), "Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(attributeCode))
                return CoreErrors.ValidationError(nameof(attributeCode), "AttributeCode cannot be empty.");

            if (sortOrder < 0)
                return CoreErrors.ValidationError(nameof(sortOrder), "SortOrder cannot be negative.");

            if (attributeGroupId == null)
                return CoreErrors.ValidationError(nameof(attributeGroupId), "AttributeGroupId cannot be null.");

            var attribute = new ProductAttribute(
                id,
                name,
                attributeCode,
                type,
                isRequired,
                isFilterable,
                showToCustomers,
                sortOrder,
                attributeGroupId
            );

            return attribute;
        }

        #endregion

        #region Methods

        public void UpdateDetails(
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder,
            AttributeGroupId attributeGroupId
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(attributeCode))
                throw new ArgumentException("AttributeCode cannot be empty.", nameof(attributeCode));

            if (sortOrder < 0)
                throw new ArgumentException("SortOrder cannot be negative.", nameof(sortOrder));

            if (attributeGroupId == null)
                throw new ArgumentException("AttributeGroupId cannot be null.", nameof(attributeGroupId));

            Name = name;
            AttributeCode = attributeCode;
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
            AttributeGroupId = attributeGroupId;
        }

        public void AddOption(AttributeOption option)
        {
            if (_options.Any(o => o.Id == option.Id))
                throw new InvalidOperationException("Option already exists.");

            _options.Add(option);
        }

        public void RemoveOption(AttributeOptionId optionId)
        {
            var option = _options.SingleOrDefault(o => o.Id == optionId);
            if (option == null)
                throw new InvalidOperationException("Option does not exist.");

            _options.Remove(option);
        }

        #endregion
    }
}
