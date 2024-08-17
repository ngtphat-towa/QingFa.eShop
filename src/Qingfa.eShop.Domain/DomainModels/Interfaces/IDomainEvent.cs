using MediatR;

namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    /// <summary>
    /// Represents a domain event within the domain model. Domain events are used to signal changes or important actions.
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
        /// Metadata is a collection of key-value pairs that can provide additional information about the event, such as source, version, or other context.
        /// </remarks>
        IReadOnlyDictionary<string, object> MetaData { get; }

        /// <summary>
        /// Flattens the event to a simplified or serializable format.
        /// </summary>
        /// <remarks>
        /// Derived classes should implement this method to transform the event into a format suitable for storage or transport.
        /// </remarks>
        string Flatten();
    }
}
