namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    public interface IEntity
    {

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the date and time when the entity was last updated, or null if it has not been updated.
        /// </summary>
        DateTime? UpdatedAt { get; }

        /// <summary>
        /// Gets the identifier of the user who created the entity, or null if unknown.
        /// </summary>
        int? CreatedBy { get; }

        /// <summary>
        /// Gets the identifier of the user who last updated the entity, or null if unknown.
        /// </summary>
        int? UpdatedBy { get; }
    }

}
