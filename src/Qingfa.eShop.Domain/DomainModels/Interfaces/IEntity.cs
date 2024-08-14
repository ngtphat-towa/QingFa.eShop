namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    /// <summary>
    /// Represents a base entity with common properties for tracking creation and updates.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public interface IEntity<TId> where TId : notnull
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        TId Id { get; set; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the date and time when the entity was last updated, or <c>null</c> if it has not been updated.
        /// </summary>
        DateTime? UpdatedAt { get; }

        /// <summary>
        /// Gets the identifier of the user who created the entity, or <c>null</c> if unknown.
        /// </summary>
        int? CreatedBy { get; }

        /// <summary>
        /// Gets the identifier of the user who last updated the entity, or <c>null</c> if unknown.
        /// </summary>
        int? UpdatedBy { get; }
    }
}
