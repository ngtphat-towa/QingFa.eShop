using System.Text.Json;
using System.Text.Json.Serialization;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Base class for domain events with common properties and behaviors.
    /// </summary>
    public abstract class Event : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        protected Event()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the full name of the event type.
        /// </summary>
        /// <remarks>
        /// This property provides the type name of the event, which can be used for categorization or identification purposes. If the type name is unavailable, it returns "UnknownEventType".
        /// </remarks>
        public string EventType => GetType().FullName ?? "UnknownEventType";

        /// <summary>
        /// Gets the date and time when the event was created.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Gets or sets a unique identifier used to correlate events.
        /// </summary>
        public string CorrelationId { get; init; } = string.Empty;

        /// <summary>
        /// Gets a collection of metadata associated with the event.
        /// </summary>
        public IReadOnlyDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Flattens the event to a simplified or serializable format.
        /// </summary>
        /// <remarks>
        /// This implementation serializes the event to a JSON string using <see cref="System.Text.Json"/>.
        /// </remarks>
        public virtual void Flatten()
        {
            // Serialize the event to JSON using System.Text.Json
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });

            // Here you could implement logic to send the JSON to a message broker or other storage.
            // For demonstration purposes, we'll just print it to the console.
            Console.WriteLine($"Flattened event: {json}");
        }
    }
}
