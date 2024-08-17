using MediatR;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a domain event within the domain model. Domain events are used to signal changes or important actions within the system.
    /// </summary>
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// Gets the date and time when the domain event was created.
        /// </summary>
        /// <remarks>
        /// This property provides a timestamp indicating when the event occurred, useful for ordering and tracking events.
        /// </remarks>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the metadata associated with the domain event.
        /// </summary>
        /// <remarks>
        /// Metadata provides additional contextual information about the event, which can be useful for handling or processing the event.
        /// </remarks>
        IReadOnlyDictionary<string, object> MetaData { get; }

        /// <summary>
        /// Flattens the domain event into a simplified or serializable format.
        /// </summary>
        /// <returns>
        /// A string representation of the event, useful for serialization, logging, or communication purposes.
        /// </returns>
        string Flatten();
    }
}
