using MediatR;

namespace QingFa.EShop.Domain.DomainModels.Interfaces;

/// <summary>
/// Represents a domain event in the domain model, which is a type of notification used
/// to signal changes or important actions within the domain.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Gets the date and time when the domain event was created.
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the metadata associated with the domain event.
    /// </summary>
    /// <remarks>
    /// Metadata is a collection of key-value pairs that can provide additional information
    /// about the event, such as source, version, or other context.
    /// </remarks>
    IDictionary<string, object> MetaData { get; }
}
