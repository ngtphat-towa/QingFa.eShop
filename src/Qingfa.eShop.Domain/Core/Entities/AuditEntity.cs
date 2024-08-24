
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Domain.Core.Entities
{
    /// <summary>
    /// Represents an entity that includes audit information and its status.
    /// </summary>
    public abstract class AuditEntity : BaseEntity<Guid>, IAuditable
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the entity.
        /// </summary>
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the status of the entity.
        /// </summary>
        public EntityStatus Status { get; set; }

        // Protected constructor for EF Core
        protected AuditEntity(Guid id)
            : base(id)
        {
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
            Status = EntityStatus.Active; // Default status
        }

        // Private parameterless constructor for EF Core
        private AuditEntity()
            : base(Guid.NewGuid())
        {
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
            Status = EntityStatus.Active; // Default status
        }

        /// <summary>
        /// Updates the audit information for the entity, including the last modified timestamp and optionally the user who made the change.
        /// </summary>
        /// <param name="lastModifiedBy">The user who last modified the entity. If <c>null</c>, the user field will not be updated.</param>
        protected void UpdateAuditInfo(string? lastModifiedBy = null)
        {
            LastModified = DateTimeOffset.UtcNow;
            LastModifiedBy = lastModifiedBy;
        }

        /// <summary>
        /// Sets the status of the entity and updates the last modified information.
        /// </summary>
        /// <param name="status">The new status to set.</param>
        /// <param name="lastModifiedBy">The user who changed the status. If <c>null</c>, the user field will not be updated.</param>
        public void SetStatus(EntityStatus? status = default, string? lastModifiedBy = null)
        {

            Status = status ?? Status;
            UpdateAuditInfo(lastModifiedBy);
        }

        /// <summary>
        /// Resets the audit information to its default values.
        /// </summary>
        public void ResetAuditInfo()
        {
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
            CreatedBy = null;
            LastModifiedBy = null;
            Status = EntityStatus.Active; // Default status
        }

        /// <summary>
        /// Checks if the entity is currently active.
        /// </summary>
        /// <returns><c>true</c> if the entity status is Active; otherwise, <c>false</c>.</returns>
        public bool IsActive()
        {
            return Status == EntityStatus.Active;
        }
    }
}
