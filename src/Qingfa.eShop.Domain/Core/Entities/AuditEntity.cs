namespace QingFa.EShop.Domain.Core.Entities
{
    /// <summary>
    /// Represents an entity that includes audit information.
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

        // Protected constructor for EF Core
        protected AuditEntity(Guid id)
            : base(id)
        {
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
        }

        // Private parameterless constructor for EF Core
        private AuditEntity()
            : base(Guid.NewGuid())
        {
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
        }

        // Method to update the last modified fields
        public void UpdateAuditInfo(string? lastModifiedBy = null)
        {
            LastModified = DateTimeOffset.UtcNow;
            LastModifiedBy = lastModifiedBy;
        }
    }
}
