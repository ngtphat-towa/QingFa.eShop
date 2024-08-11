using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels.Bases;

/// <summary>
/// Represents a base class for entities in the domain model, providing common properties such as Id, Created, and Updated timestamps.
/// </summary>
public abstract class EntityBase : IEntity
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    public DateTime Created { get; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the entity has not been updated since creation.
    /// </remarks>
    public DateTime? Updated { get; protected set; }
}
