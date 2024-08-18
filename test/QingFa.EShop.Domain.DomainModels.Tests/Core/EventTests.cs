using System.Text.Json;
using System.Text.Json.Serialization;

using FluentAssertions;

using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Domain.Tests.Core
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

            var expectedEventType = "QingFa.EShop.Domain.Tests.Core.EventTests+TestEvent";
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

            var actualJson = testEvent.Flatten();
            var expectedJson = JsonSerializer.Serialize(new
            {
                testEvent.EventType,
                CreatedAt = testEvent.CreatedAt.ToString("o"),
                testEvent.CorrelationId,
                testEvent.MetaData
            }, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });

            actualJson.Should().Contain(expectedJson);
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

            var actualJson = testEvent.Flatten();
            var expectedJson = JsonSerializer.Serialize(new
            {
                testEvent.EventType,
                CreatedAt = testEvent.CreatedAt.ToString("o"),
                testEvent.CorrelationId,
                MetaData = metaData
            }, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });

            // Use Contains to check the JSON content ignoring the timestamp part
            actualJson.Should().Contain(expectedJson);
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
            Thread.Sleep(1000); // Ensure a different time
            var testEvent2 = new TestEvent("correlation-id");

            var json1 = testEvent1.Flatten();
            var json2 = testEvent2.Flatten();

            json1.Should().NotBe(json2); // Ensures different timestamps
        }

        //[Fact]
        //public void Flatten_ShouldHandleEmptyMetadata()
        //{
        //    var testEvent = new TestEvent("correlation-id", new Dictionary<string, object>());

        //    var expectedJson = JsonSerializer.Serialize(new
        //    {
        //        testEvent.EventType,
        //        CreatedAt = testEvent.CreatedAt.ToString("o"),
        //        testEvent.CorrelationId,
        //        MetaData = new Dictionary<string, object>()
        //    }, new JsonSerializerOptions
        //    {
        //        WriteIndented = false,
        //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        //        Converters = { new JsonStringEnumConverter() }
        //    });

        //    var actualJson = testEvent.Flatten();

        //    actualJson.Should().Be(expectedJson);
        //}

        [Fact]
        public void Flatten_ShouldHandleNullMetadata()
        {
            var testEvent = new TestEvent("correlation-id", null);

            var expectedJson = JsonSerializer.Serialize(new
            {
                testEvent.EventType,
                CreatedAt = testEvent.CreatedAt.ToString("o"),
                testEvent.CorrelationId,
                MetaData = new Dictionary<string, object>()
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
        public void Flatten_ShouldHandleLargeMetadata()
        {
            var metaData = new Dictionary<string, object>();
            for (int i = 0; i < 1000; i++)
            {
                metaData[$"key{i}"] = $"value{i}";
            }
            var testEvent = new TestEvent("correlation-id", metaData);

            var expectedJson = JsonSerializer.Serialize(new
            {
                testEvent.EventType,
                CreatedAt = testEvent.CreatedAt.ToString("o"),
                testEvent.CorrelationId,
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
        public void SerializeTestEvent_WithSpecialCharacters_ShouldIncludeAllProperties()
        {
            var specialCharacters = new Dictionary<string, object>
            {
                { "key with spaces", "value with spaces" },
                { "key@special#chars!", "value@special#chars!" }
            };
            var testEvent = new TestEvent("correlation-id", specialCharacters);

            string json = JsonSerializer.Serialize(testEvent, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            });

            json.Should().Contain("\"key with spaces\":\"value with spaces\"");
            json.Should().Contain("\"key@special#chars!\":\"value@special#chars!\"");
        }
    }
}
