using ErrorOr;

using MediatR;

using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Attributes
{
    /// <summary>
    /// Represents an option for a specific attribute.
    /// </summary>
    public class AttributeOption : Entity<AttributeOptionId>, IEquatable<AttributeOption>
    {
        #region Properties

        /// <summary>
        /// Gets the ID of the attribute this option belongs to.
        /// </summary>
        public ProductAttributeId AttributeId { get; private set; }

        /// <summary>
        /// Gets the value of the option.
        /// </summary>
        public string OptionValue { get; private set; }

        /// <summary>
        /// Gets the description of the option.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Indicates whether this option is the default option.
        /// </summary>
        public bool IsDefault { get; private set; }

        /// <summary>
        /// Gets the sort order of the option.
        /// </summary>
        public int SortOrder { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeOption"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the option.</param>
        /// <param name="attributeId">The ID of the attribute this option belongs to.</param>
        /// <param name="optionValue">The value of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="isDefault">Indicates whether this option is the default option.</param>
        /// <param name="sortOrder">The sort order of the option.</param>
        protected AttributeOption(
            AttributeOptionId id,
            ProductAttributeId attributeId,
            string optionValue,
            string description,
            bool isDefault,
            int sortOrder
        ) : base(id)
        {
            OptionValue = optionValue;
            Description = description;
            IsDefault = isDefault;
            SortOrder = sortOrder;
            AttributeId = attributeId;
        }

        /// <summary>
        /// Parameterless constructor for EF Core.
        /// </summary>
#pragma warning disable CS8618
        protected AttributeOption() : base(default!) { }
#pragma warning restore CS8618

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method to create a new instance of <see cref="AttributeOption"/> with validation.
        /// </summary>
        /// <param name="id">The unique identifier of the option.</param>
        /// <param name="attributeId">The ID of the attribute this option belongs to.</param>
        /// <param name="optionValue">The value of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="isDefault">Indicates whether this option is the default option.</param>
        /// <param name="sortOrder">The sort order of the option.</param>
        /// <returns>An <see cref="ErrorOr{AttributeOption}"/> containing either the new instance or a validation error.</returns>
        public static ErrorOr<AttributeOption> Create(
            AttributeOptionId id,
            ProductAttributeId attributeId,
            string optionValue,
            string description,
            bool isDefault,
            int sortOrder
        )
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(optionValue))
                errors.Add(CoreErrors.ValidationError(nameof(optionValue), "OptionValue cannot be empty."));

            if (string.IsNullOrWhiteSpace(description))
                errors.Add(CoreErrors.ValidationError(nameof(description), "Description cannot be empty."));

            if (sortOrder < 0)
                errors.Add(CoreErrors.ValidationError(nameof(sortOrder), "SortOrder cannot be negative."));

            if (errors.Any())
                return errors;

            var attributeOption = new AttributeOption(
                id,
                attributeId,
                optionValue,
                description,
                isDefault,
                sortOrder
            );

            return attributeOption;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the details of the option.
        /// </summary>
        /// <param name="optionValue">The new value of the option.</param>
        /// <param name="description">The new description of the option.</param>
        /// <param name="isDefault">Indicates whether this option is the default option.</param>
        /// <param name="sortOrder">The new sort order of the option.</param>
        /// <returns>An <see cref="ErrorOr{Unit}"/> indicating the result of the operation.</returns>
        public ErrorOr<Unit> UpdateDetails(
            string optionValue,
            string description,
            bool isDefault,
            int sortOrder
        )
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(optionValue))
                errors.Add(CoreErrors.ValidationError(nameof(optionValue), "OptionValue cannot be empty."));

            if (string.IsNullOrWhiteSpace(description))
                errors.Add(CoreErrors.ValidationError(nameof(description), "Description cannot be empty."));

            if (sortOrder < 0)
                errors.Add(CoreErrors.ValidationError(nameof(sortOrder), "SortOrder cannot be negative."));

            if (errors.Any())
                return errors;

            OptionValue = optionValue;
            Description = description;
            IsDefault = isDefault;
            SortOrder = sortOrder;

            return Unit.Value; // Indicating success
        }

        #endregion

        #region Equality

        public bool Equals(AttributeOption? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            // Compare all relevant properties for equality
            return Id.Equals(other.Id)
                && AttributeId.Equals(other.AttributeId)
                && OptionValue == other.OptionValue
                && Description == other.Description
                && IsDefault == other.IsDefault
                && SortOrder == other.SortOrder;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AttributeOption);
        }

        public override int GetHashCode()
        {
            // Combine properties into hash codes
            return HashCode.Combine(Id, AttributeId, OptionValue, Description, IsDefault, SortOrder);
        }

        #endregion
    }
}
