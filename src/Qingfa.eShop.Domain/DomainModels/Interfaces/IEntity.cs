namespace QingFa.EShop.Domain.DomainModels.Interfaces;

/// <summary>
/// Defines the base properties for an entity in the domain model.
/// </summary>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    TId Id { get; }

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedTime { get; }

    /// <summary>
    /// Gets the date and time when the entity was last updated.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the entity has not been updated since creation.
    /// </remarks>
    DateTime? UpdatedTime { get; }
}
