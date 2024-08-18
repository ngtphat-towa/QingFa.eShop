using ErrorOr;

using MediatR;

using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Attributes
{
    /// <summary>
    /// Represents a product attribute in the catalog.
    /// </summary>
    public class ProductAttribute : Entity<ProductAttributeId>, IEquatable<ProductAttribute>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the product attribute.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the attribute code for the product attribute.
        /// </summary>
        public string AttributeCode { get; private set; }

        /// <summary>
        /// Gets the type of the product attribute.
        /// </summary>
        public AttributeType Type { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the attribute is required.
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the attribute is filterable.
        /// </summary>
        public bool IsFilterable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the attribute is shown to customers.
        /// </summary>
        public bool ShowToCustomers { get; private set; }

        /// <summary>
        /// Gets the sort order of the product attribute.
        /// </summary>
        public int SortOrder { get; private set; }

        /// <summary>
        /// Gets the ID of the attribute group this attribute belongs to.
        /// </summary>
        public AttributeGroupId AttributeGroupId { get; private set; }

        private readonly List<AttributeOption> _options = new();
        public IReadOnlyList<AttributeOption> Options => _options.AsReadOnly();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAttribute"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the product attribute.</param>
        /// <param name="name">The name of the product attribute.</param>
        /// <param name="attributeCode">The attribute code for the product attribute.</param>
        /// <param name="type">The type of the product attribute.</param>
        /// <param name="isRequired">Indicates whether the attribute is required.</param>
        /// <param name="isFilterable">Indicates whether the attribute is filterable.</param>
        /// <param name="showToCustomers">Indicates whether the attribute is shown to customers.</param>
        /// <param name="sortOrder">The sort order of the product attribute.</param>
        /// <param name="attributeGroupId">The ID of the attribute group this attribute belongs to.</param>
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

        /// <summary>
        /// Parameterless constructor for EF Core.
        /// </summary>
#pragma warning disable CS8618
        protected ProductAttribute() : base(default!) { }
#pragma warning restore CS8618

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method to create a new instance of <see cref="ProductAttribute"/> with validation.
        /// </summary>
        /// <param name="id">The unique identifier of the product attribute.</param>
        /// <param name="name">The name of the product attribute.</param>
        /// <param name="attributeCode">The attribute code for the product attribute.</param>
        /// <param name="type">The type of the product attribute.</param>
        /// <param name="isRequired">Indicates whether the attribute is required.</param>
        /// <param name="isFilterable">Indicates whether the attribute is filterable.</param>
        /// <param name="showToCustomers">Indicates whether the attribute is shown to customers.</param>
        /// <param name="sortOrder">The sort order of the product attribute.</param>
        /// <param name="attributeGroupId">The ID of the attribute group this attribute belongs to.</param>
        /// <returns>An <see cref="ErrorOr{ProductAttribute}"/> containing either the new instance or a validation error.</returns>
        public static ErrorOr<ProductAttribute> Create(
            ProductAttributeId id,
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder,
            AttributeGroupId attributeGroupId)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(name))
                errors.Add(CoreErrors.ValidationError(nameof(name), "Name cannot be empty."));

            if (string.IsNullOrWhiteSpace(attributeCode))
                errors.Add(CoreErrors.ValidationError(nameof(attributeCode), "AttributeCode cannot be empty."));

            if (sortOrder < 0)
                errors.Add(CoreErrors.ValidationError(nameof(sortOrder), "SortOrder cannot be negative."));

            if (attributeGroupId == null)
                errors.Add(CoreErrors.ValidationError(nameof(attributeGroupId), "AttributeGroupId cannot be null."));

            if (errors.Any())
                return errors;

            return new ProductAttribute(id, name, attributeCode, type, isRequired, isFilterable, showToCustomers, sortOrder, attributeGroupId);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the details of the product attribute.
        /// </summary>
        /// <param name="name">The new name of the product attribute.</param>
        /// <param name="attributeCode">The new attribute code for the product attribute.</param>
        /// <param name="type">The new type of the product attribute.</param>
        /// <param name="isRequired">Indicates whether the attribute is required.</param>
        /// <param name="isFilterable">Indicates whether the attribute is filterable.</param>
        /// <param name="showToCustomers">Indicates whether the attribute is shown to customers.</param>
        /// <param name="sortOrder">The new sort order of the product attribute.</param>
        /// <param name="attributeGroupId">The new ID of the attribute group this attribute belongs to.</param>
        /// <returns>An <see cref="ErrorOr{Unit}"/> indicating the result of the operation.</returns>
        public ErrorOr<Unit> UpdateDetails(
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder,
            AttributeGroupId attributeGroupId)
        {
            if (string.IsNullOrWhiteSpace(name))
                return CoreErrors.ValidationError(nameof(name), "Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(attributeCode))
                return CoreErrors.ValidationError(nameof(attributeCode), "AttributeCode cannot be empty.");

            if (sortOrder < 0)
                return CoreErrors.ValidationError(nameof(sortOrder), "SortOrder cannot be negative.");

            if (attributeGroupId == null)
                return CoreErrors.ValidationError(nameof(attributeGroupId), "AttributeGroupId cannot be null.");

            Name = name;
            AttributeCode = attributeCode;
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
            AttributeGroupId = attributeGroupId;

            return Unit.Value; // Indicating success
        }

        /// <summary>
        /// Adds an option to the product attribute.
        /// </summary>
        /// <param name="option">The option to add.</param>
        /// <returns>An <see cref="ErrorOr{Unit}"/> indicating the result of the operation.</returns>
        public ErrorOr<Unit> AddOption(AttributeOption option)
        {
            if (option.AttributeId != Id)
                return CoreErrors.ValidationError(nameof(option), "Option does not belong to this attribute.");

            if (_options.Any(o => o.Id == option.Id))
                return CoreErrors.ValidationError(nameof(option), "Option already exists.");

            _options.Add(option);

            return Unit.Value; // Indicating success
        }

        /// <summary>
        /// Removes an option from the product attribute.
        /// </summary>
        /// <param name="optionId">The ID of the option to remove.</param>
        /// <returns>An <see cref="ErrorOr{Unit}"/> indicating the result of the operation.</returns>
        public ErrorOr<Unit> RemoveOption(AttributeOptionId optionId)
        {
            var option = _options.SingleOrDefault(o => o.Id == optionId);
            if (option == null)
                return CoreErrors.ValidationError(nameof(optionId), "Option does not exist.");

            _options.Remove(option);

            return Unit.Value; // Indicating success
        }

        #endregion

        #region Equality

        public bool Equals(ProductAttribute? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id.Equals(other.Id)
                && Name == other.Name
                && AttributeCode == other.AttributeCode
                && Type == other.Type
                && IsRequired == other.IsRequired
                && IsFilterable == other.IsFilterable
                && ShowToCustomers == other.ShowToCustomers
                && SortOrder == other.SortOrder
                && AttributeGroupId.Equals(other.AttributeGroupId);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ProductAttribute);
        }

        public override int GetHashCode()
        {
            // Combine properties into hash codes
            int hash1 = HashCode.Combine(Id, Name, AttributeCode, Type);
            int hash2 = HashCode.Combine(IsRequired, IsFilterable, ShowToCustomers, SortOrder, AttributeGroupId);

            // Combine the two hash codes
            return HashCode.Combine(hash1, hash2);
        }

        #endregion
    }
}
