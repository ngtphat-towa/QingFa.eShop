namespace QingFa.EShop.Domain.Common.Enums
{
    /// <summary>
    /// Represents the status of an entity in the system.
    /// </summary>
    public enum EntityStatus
    {
        /// <summary>
        /// The entity is active and currently in use.
        /// This status indicates that the entity is available for interaction or processing.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The entity is inactive and not currently in use.
        /// This status indicates that the entity is not available for interaction but may be reactivated later.
        /// </summary>
        Inactive = 2,

        /// <summary>
        /// The entity is archived and no longer actively used.
        /// This status indicates that the entity has been moved to archival storage and is not accessible for regular operations.
        /// </summary>
        Archived = 3,

        /// <summary>
        /// The entity is deleted and no longer exists in the system.
        /// This status indicates that the entity has been permanently removed from the system and cannot be recovered.
        /// </summary>
        Deleted = 4,

        /// <summary>
        /// The entity is pending and awaiting some action or approval.
        /// This status indicates that the entity is in a state of waiting for further processing or review.
        /// </summary>
        Pending = 5
    }
}
