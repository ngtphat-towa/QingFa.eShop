using QingFa.EShop.Domain.DomainModels.Interfaces;

public abstract class Entity<TId> : IEntity<TId>
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    public TId Id { get; }

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedTime { get; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the entity has not been updated since creation.
    /// </remarks>
    public DateTime? UpdatedTime { get; protected set; }

    // Constructor that requires Id to be passed
    protected Entity(TId id)
    {
        Id = id;
    }
}