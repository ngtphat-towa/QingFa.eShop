using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Domain.Core.Entities
{
    /// <summary>
    /// Defines the properties required for auditing an entity, including creation and modification information, and its status.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the entity.
        /// </summary>
        string? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the status of the entity.
        /// </summary>
        EntityStatus Status { get; set; }
    }
}
