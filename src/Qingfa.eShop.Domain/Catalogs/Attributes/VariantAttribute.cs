using ErrorOr;

using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

using System;
using System.Collections.Generic;
using System.Linq;

namespace QingFa.EShop.Domain.Catalogs.Attributes
{
    /// <summary>
    /// Represents a variant attribute in the catalog.
    /// </summary>
    public class VariantAttribute : Entity<AttributeId>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the code of the attribute.
        /// </summary>
        public string AttributeCode { get; private set; }

        /// <summary>
        /// Gets the type of the attribute.
        /// </summary>
        public AttributeType Type { get; private set; }

        /// <summary>
        /// Indicates whether the attribute is required.
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// Indicates whether the attribute is filterable.
        /// </summary>
        public bool IsFilterable { get; private set; }

        /// <summary>
        /// Indicates whether the attribute is shown to customers.
        /// </summary>
        public bool ShowToCustomers { get; private set; }

        /// <summary>
        /// Gets the sort order of the attribute.
        /// </summary>
        public int SortOrder { get; private set; }

        private readonly List<AttributeOption> _options = new();
        /// <summary>
        /// List of options associated with this attribute.
        /// </summary>
        public IReadOnlyList<AttributeOption> Options => _options.AsReadOnly();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VariantAttribute"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="attributeCode">The code of the attribute.</param>
        /// <param name="type">The type of the attribute.</param>
        /// <param name="isRequired">Whether the attribute is required.</param>
        /// <param name="isFilterable">Whether the attribute is filterable.</param>
        /// <param name="showToCustomers">Whether the attribute is shown to customers.</param>
        /// <param name="sortOrder">The sort order of the attribute.</param>
        protected VariantAttribute(
            AttributeId id,
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder
        ) : base(id)
        {
            Name = name;
            AttributeCode = attributeCode;
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Parameterless constructor for EF Core.
        /// </summary>
#pragma warning disable CS8618
        protected VariantAttribute() : base(default!) { }
#pragma warning restore CS8618 

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method to create a new instance of <see cref="VariantAttribute"/> with validation.
        /// </summary>
        /// <param name="id">The unique identifier of the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="attributeCode">The code of the attribute.</param>
        /// <param name="type">The type of the attribute.</param>
        /// <param name="isRequired">Whether the attribute is required.</param>
        /// <param name="isFilterable">Whether the attribute is filterable.</param>
        /// <param name="showToCustomers">Whether the attribute is shown to customers.</param>
        /// <param name="sortOrder">The sort order of the attribute.</param>
        /// <returns>An <see cref="ErrorOr{VariantAttribute}"/> containing either the new instance or a validation error.</returns>
        public static ErrorOr<VariantAttribute> Create(
            AttributeId id,
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                return CoreErrors.ValidationError(nameof(name), "Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(attributeCode))
                return CoreErrors.ValidationError(nameof(attributeCode), "AttributeCode cannot be empty.");

            if (sortOrder < 0)
                return CoreErrors.ValidationError(nameof(sortOrder), "SortOrder cannot be negative.");

            var attribute = new VariantAttribute(
                id,
                name,
                attributeCode,
                type,
                isRequired,
                isFilterable,
                showToCustomers,
                sortOrder
            );

            return attribute;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the details of the attribute.
        /// </summary>
        /// <param name="name">The new name of the attribute.</param>
        /// <param name="attributeCode">The new code of the attribute.</param>
        /// <param name="type">The new type of the attribute.</param>
        /// <param name="isRequired">Whether the attribute is required.</param>
        /// <param name="isFilterable">Whether the attribute is filterable.</param>
        /// <param name="showToCustomers">Whether the attribute is shown to customers.</param>
        /// <param name="sortOrder">The new sort order of the attribute.</param>
        public void UpdateDetails(
            string name,
            string attributeCode,
            AttributeType type,
            bool isRequired,
            bool isFilterable,
            bool showToCustomers,
            int sortOrder
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(attributeCode))
                throw new ArgumentException("AttributeCode cannot be empty.", nameof(attributeCode));

            if (sortOrder < 0)
                throw new ArgumentException("SortOrder cannot be negative.", nameof(sortOrder));

            Name = name;
            AttributeCode = attributeCode;
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Adds an option to the attribute.
        /// </summary>
        /// <param name="option">The option to add.</param>
        public void AddOption(AttributeOption option)
        {
            if (_options.Any(o => o.Id == option.Id))
                throw new InvalidOperationException("Option already exists.");

            _options.Add(option);
        }

        /// <summary>
        /// Removes an option from the attribute.
        /// </summary>
        /// <param name="optionId">The ID of the option to remove.</param>
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
