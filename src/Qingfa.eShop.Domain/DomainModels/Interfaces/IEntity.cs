namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Defines the base properties for an entity in the domain model.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the unique identifier of the entity.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Gets the date and time when the entity was last updated.
        /// </summary>
        /// <remarks>
        /// This property may be <c>null</c> if the entity has not been updated since creation.
        /// </remarks>
        DateTime? Updated { get; }
    }
}
