using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a base class for domain events with common properties and behaviors.
    /// </summary>
    public abstract class EventBase : IDomainEvent
    {
        /// <summary>
        /// Gets the full name of the event type.
        /// </summary>
        /// <remarks>
        /// This property provides the type name of the event, which can be used for
        /// categorization or identification purposes. If the type name is unavailable,
        /// it returns "UnknownEventType".
        /// </remarks>
        public string EventType => GetType().FullName ?? "UnknownEventType";

        /// <summary>
        /// Gets the date and time when the event was created.
        /// </summary>
        /// <remarks>
        /// This property is initialized to the current UTC time when the event instance
        /// is created.
        /// </remarks>
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets a unique identifier used to correlate events.
        /// </summary>
        /// <remarks>
        /// This property can be used to track or correlate related events across
        /// different systems or processes. It is initialized to an empty string by default.
        /// </remarks>
        public string CorrelationId { get; init; } = string.Empty;

        /// <summary>
        /// Gets a collection of metadata associated with the event.
        /// </summary>
        /// <remarks>
        /// This dictionary can store additional information about the event, such as
        /// contextual details or custom attributes. The metadata is provided as key-value pairs.
        /// </remarks>
        public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Flattens the event to a more simplified or serializable format.
        /// </summary>
        /// <remarks>
        /// Derived classes should implement this method to transform the event into
        /// a format suitable for storage or transport, such as a data transfer object (DTO).
        /// </remarks>
        public abstract void Flatten();
    }
}
