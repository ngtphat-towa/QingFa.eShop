using FluentAssertions;

using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Tests.DomainModels
{
    // Define a concrete implementation of Entity for testing purposes
    public class TestEntity : Entity<int>
    {
        public TestEntity(int id) : base(id) { }
    }

    public class EntityTests
    {
        [Fact]
        public void HandleEntityInitialization_WhenCreated_ShouldSetCreatedAtToUtcNow()
        {
            // Arrange
            var id = 1;

            // Act
            var testEntity = new TestEntity(id);
            var createdAt = testEntity.CreatedAt;

            // Assert
            createdAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void HandleEntityId_WhenInitialized_ShouldBeSetCorrectly()
        {
            // Arrange
            var id = 1;

            // Act
            var testEntity = new TestEntity(id);

            // Assert
            testEntity.Id.Should().Be(id);
        }

        [Fact]
        public void HandleEntityCreatedAt_WhenCreated_ShouldNotBeDefault()
        {
            // Arrange
            var id = 1;

            // Act
            var testEntity = new TestEntity(id);

            // Assert
            testEntity.CreatedAt.Should().NotBe(default(DateTime));
        }

        [Fact]
        public void HandleEntityUpdateTimestamp_WhenCalled_ShouldSetUpdatedAtToUtcNow()
        {
            // Arrange
            var id = 1;
            var testEntity = new TestEntity(id);

            // Act
            testEntity.UpdateTimestamp();
            var updatedAt = testEntity.UpdatedAt;

            // Assert
            updatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void HandleEntityUpdateTimestamp_WhenNotCalled_ShouldHaveNullUpdatedAt()
        {
            // Arrange
            var id = 1;
            var testEntity = new TestEntity(id);

            // Act
            var updatedAt = testEntity.UpdatedAt;

            // Assert
            updatedAt.Should().BeNull();
        }

        [Fact]
        public void HandleAddDomainEvent_WhenAdded_ShouldBeInDomainEventsList()
        {
            // Arrange
            var id = 1;
            var testEntity = new TestEntity(id);
            var testEvent = new TestEvent("correlation-id");

            // Act
            testEntity.AddDomainEvent(testEvent);

            // Assert
            testEntity.DomainEvents.Should().Contain(testEvent);
        }

        [Fact]
        public void HandleRemoveDomainEvent_WhenRemoved_ShouldNotBeInDomainEventsList()
        {
            // Arrange
            var id = 1;
            var testEntity = new TestEntity(id);
            var testEvent = new TestEvent("correlation-id");

            // Act
            testEntity.AddDomainEvent(testEvent);
            testEntity.RemoveDomainEvent(testEvent);

            // Assert
            testEntity.DomainEvents.Should().NotContain(testEvent);
        }

        [Fact]
        public void HandleClearDomainEvents_WhenCleared_ShouldBeEmpty()
        {
            // Arrange
            var id = 1;
            var testEntity = new TestEntity(id);
            var testEvent = new TestEvent("correlation-id");

            // Act
            testEntity.AddDomainEvent(testEvent);
            testEntity.ClearDomainEvents();

            // Assert
            testEntity.DomainEvents.Should().BeEmpty();
        }

        [Fact]
        public void HandleEntityEquality_WhenEqual_ShouldReturnTrue()
        {
            // Arrange
            var id = 1;
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            // Assert
            entity1.Equals(entity2).Should().BeTrue();
        }

        [Fact]
        public void HandleEntityEquality_WhenNotEqual_ShouldReturnFalse()
        {
            // Arrange
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            // Assert
            entity1.Equals(entity2).Should().BeFalse();
        }

        [Fact]
        public void HandleEntityEquality_WithDifferentType_ShouldReturnFalse()
        {
            // Arrange
            var entity = new TestEntity(1);
            var otherObject = new object();

            // Assert
            entity.Equals(otherObject).Should().BeFalse();
        }

        [Fact]
        public void HandleEntityHashCode_WhenEqualEntities_ShouldBeSame()
        {
            // Arrange
            var id = 1;
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            // Assert
            entity1.GetHashCode().Should().Be(entity2.GetHashCode());
        }

        [Fact]
        public void HandleEntityHashCode_WhenDifferentEntities_ShouldBeDifferent()
        {
            // Arrange
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            // Assert
            entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
        }
    }

    // Define a concrete implementation of Event for testing purposes
    public class TestEvent : IDomainEvent
    {
        public TestEvent(string correlationId, Dictionary<string, object>? metaData = null)
        {
            CorrelationId = correlationId;
            MetaData = metaData ?? new Dictionary<string, object>();
            CreatedAt = DateTime.UtcNow;
        }

        public string CorrelationId { get; }
        public DateTime CreatedAt { get; }
        public IReadOnlyDictionary<string, object> MetaData { get; }

        public string Flatten()
        {
            return $"CorrelationId: {CorrelationId}, CreatedAt: {CreatedAt}, MetaData: {string.Join(", ", MetaData)}";
        }
    }
}
