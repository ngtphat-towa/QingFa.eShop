namespace QingFa.EShop.Domain.Core.Enums
{
    /// <summary>
    /// Represents the status of an entity in the system.
    /// </summary>
    public enum EntityStatus : short
    {
        /// <summary>
        /// Indicates that the entity is active and in use.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Indicates that the entity is inactive and not currently in use.
        /// </summary>
        Inactive = 2,

        /// <summary>
        /// Indicates that the entity is suspended and temporarily unavailable.
        /// </summary>
        Suspended = 3,

        /// <summary>
        /// Indicates that the entity has been deleted and is no longer available.
        /// </summary>
        Deleted = 4
    }
}
