using System.Text.Json;
using System.Text.Json.Serialization;

using FluentAssertions;

using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Tests.DomainModels
{
    public class EventTests
    {
        private class TestEvent : Event
        {
            public TestEvent(string correlationId, Dictionary<string, object>? metaData = null)
                : base(metaData)
            {
                CorrelationId = correlationId;
            }

            public string TestProperty { get; set; } = "TestValue";
        }

        [Fact]
        public void EventInitialization_ShouldSetCreatedAtToUtcNow()
        {
            var testEvent = new TestEvent("correlation-id");

            var createdAt = testEvent.CreatedAt;

            createdAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void EventType_ShouldReturnFullName()
        {
            var testEvent = new TestEvent("correlation-id");

            // Update expected value based on actual namespace
            var expectedEventType = "QingFa.EShop.Domain.Tests.DomainModels.EventTests+TestEvent";
            var eventType = testEvent.EventType;

            eventType.Should().Be(expectedEventType);
        }


        [Fact]
        public void CorrelationId_ShouldBeInitialized()
        {
            var correlationId = "test-correlation-id";
            var testEvent = new TestEvent(correlationId);

            var result = testEvent.CorrelationId;

            result.Should().Be(correlationId);
        }

        [Fact]
        public void Flatten_ShouldSerializeEventToJson()
        {
            var metaData = new Dictionary<string, object>
    {
        { "key1", "value1" },
        { "key2", 123 }
    };
            var testEvent = new TestEvent("correlation-id", metaData);

            // Manually serialize the expected JSON
            string expectedJson = JsonSerializer.Serialize(new
            {
                EventType = testEvent.EventType,
                CreatedAt = testEvent.CreatedAt.ToString("o"), // Use the "o" format specifier for ISO 8601
                CorrelationId = testEvent.CorrelationId,
                MetaData = testEvent.MetaData
            }, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });

            // Use Flatten method to get the actual JSON
            string actualJson = testEvent.Flatten();

            // Compare the actual JSON with the expected JSON
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void SerializeTestEvent_ShouldIncludeAllProperties()
        {
            var metaData = new Dictionary<string, object>
    {
        { "key1", "value1" },
        { "key2", 123 }
    };
            var testEvent = new TestEvent("correlation-id", metaData);

            string json = JsonSerializer.Serialize(testEvent, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });

            // Verify that the JSON includes the TestProperty
            json.Should().Contain("\"TestProperty\":\"TestValue\"");
        }

        [Fact]
        public void Flatten_WhenMetadataContainsComplexObjects_ShouldSerializeCorrectly()
        {
            var metaData = new Dictionary<string, object>
    {
        { "key1", new { NestedKey = "NestedValue" } },
        { "key2", new List<int> { 1, 2, 3 } }
    };
            var testEvent = new TestEvent("correlation-id", metaData);

            var expectedJson = JsonSerializer.Serialize(new
            {
                EventType = testEvent.EventType,
                CreatedAt = testEvent.CreatedAt.ToString("o"),
                CorrelationId = testEvent.CorrelationId,
                MetaData = metaData
            }, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });

            var actualJson = testEvent.Flatten();

            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void EventInitialization_ShouldInitializeAdditionalProperties()
        {
            var testEvent = new TestEvent("correlation-id");

            testEvent.TestProperty.Should().Be("TestValue");
        }

        [Fact]
        public void Flatten_ShouldHandleDifferentCreationTimes()
        {
            var testEvent1 = new TestEvent("correlation-id");
            System.Threading.Thread.Sleep(1000); // Ensure a different time
            var testEvent2 = new TestEvent("correlation-id");

            var json1 = testEvent1.Flatten();
            var json2 = testEvent2.Flatten();

            json1.Should().NotBe(json2); // Ensures different timestamps
        }

    }
}
