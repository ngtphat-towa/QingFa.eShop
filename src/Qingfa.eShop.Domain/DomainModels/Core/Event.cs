using System.Text.Json;
using System.Text.Json.Serialization;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels.Core
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
            MetaData = new Dictionary<string, object>();
        }

        protected Event(Dictionary<string, object>? metaData = null)
        {
            CreatedAt = DateTime.UtcNow;
            MetaData = metaData ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets the full name of the event type.
        /// </summary>
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
        public IReadOnlyDictionary<string, object> MetaData { get; }

        /// <summary>
        /// Flattens the event to a simplified or serializable format.
        /// </summary>
        public virtual string Flatten()
        {
            // Serialize the entire object, including all properties
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });
        }


    }
}
