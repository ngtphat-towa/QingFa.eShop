using ErrorOr;

using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Attributes
{
    /// <summary>
    /// Represents a group of related attributes.
    /// </summary>
    public class AttributeGroup : Entity<AttributeGroupId>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the attribute group.
        /// </summary>
        public string GroupName { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeGroup"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the attribute group.</param>
        /// <param name="groupName">The name of the attribute group.</param>
        protected AttributeGroup(
            AttributeGroupId id,
            string groupName
        ) : base(id)
        {
            GroupName = groupName;
        }

        /// <summary>
        /// Parameterless constructor for EF Core.
        /// </summary>
#pragma warning disable CS8618 
        protected AttributeGroup() : base(default!) { }
#pragma warning restore CS8618 

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method to create a new instance of <see cref="AttributeGroup"/> with validation.
        /// </summary>
        /// <param name="id">The unique identifier of the attribute group.</param>
        /// <param name="groupName">The name of the attribute group.</param>
        /// <returns>A new instance of <see cref="AttributeGroup"/> or a validation error.</returns>
        public static ErrorOr<AttributeGroup> Create(
            AttributeGroupId id,
            string groupName
        )
        {
            if (string.IsNullOrWhiteSpace(groupName))
                return CoreErrors.ValidationError(nameof(groupName), "GroupName cannot be empty.");

            var attributeGroup = new AttributeGroup(
                id,
                groupName
            );

            return attributeGroup;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the details of the attribute group.
        /// </summary>
        /// <param name="groupName">The new name of the attribute group.</param>
        public void UpdateDetails(
            string groupName
        )
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentException("GroupName cannot be empty.", nameof(groupName));

            GroupName = groupName;
        }

        #endregion
    }
}
