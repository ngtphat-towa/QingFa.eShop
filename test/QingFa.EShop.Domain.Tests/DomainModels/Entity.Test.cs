using FluentAssertions;

using Moq;

using QingFa.EShop.Domain.DomainModels;
using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.Tests.DomainModels
{
    public class EntityTests
    {
        private class TestEntity : Entity<int>
        {
            public TestEntity(int id) : base(id) { }
            public TestEntity() : base() { }
        }

        [Fact]
        public void Constructor_WithId_ShouldInitializeId()
        {
            // Arrange
            var id = 1;

            // Act
            var entity = new TestEntity(id);

            // Assert
            entity.Id.Should().Be(id);
            entity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(1));
            entity.UpdatedAt.Should().BeNull();
            entity.CreatedBy.Should().BeNull();
            entity.UpdatedBy.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithoutId_ShouldInitializeIdToDefault()
        {
            // Act
            var entity = new TestEntity();

            // Assert
            entity.Id.Should().Be(0);
            entity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(1));
            entity.UpdatedAt.Should().BeNull();
            entity.CreatedBy.Should().BeNull();
            entity.UpdatedBy.Should().BeNull();
        }

        [Fact]
        public void AddDomainEvent_ShouldAddEventToDomainEvents()
        {
            // Arrange
            var entity = new TestEntity();
            var domainEventMock = new Mock<IDomainEvent>();

            // Act
            entity.AddDomainEvent(domainEventMock.Object);

            // Assert
            entity.DomainEvents.Should().Contain(domainEventMock.Object);
        }

        [Fact]
        public void RemoveDomainEvent_ShouldRemoveEventFromDomainEvents()
        {
            // Arrange
            var entity = new TestEntity();
            var domainEventMock = new Mock<IDomainEvent>();
            entity.AddDomainEvent(domainEventMock.Object);

            // Act
            entity.RemoveDomainEvent(domainEventMock.Object);

            // Assert
            entity.DomainEvents.Should().NotContain(domainEventMock.Object);
        }

        [Fact]
        public void ClearDomainEvents_ShouldRemoveAllDomainEvents()
        {
            // Arrange
            var entity = new TestEntity();
            var domainEventMock1 = new Mock<IDomainEvent>();
            var domainEventMock2 = new Mock<IDomainEvent>();
            entity.AddDomainEvent(domainEventMock1.Object);
            entity.AddDomainEvent(domainEventMock2.Object);

            // Act
            entity.ClearDomainEvents();

            // Assert
            entity.DomainEvents.Should().BeEmpty();
        }

        [Fact]
        public void GetDomainEventCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var entity = new TestEntity();
            var domainEventMock1 = new Mock<IDomainEvent>();
            var domainEventMock2 = new Mock<IDomainEvent>();
            entity.AddDomainEvent(domainEventMock1.Object);
            entity.AddDomainEvent(domainEventMock2.Object);

            // Act
            var count = entity.GetDomainEventCount();

            // Assert
            count.Should().Be(2);
        }

        [Fact]
        public void AddSameDomainEventMultipleTimes_ShouldContainEventMultipleTimes()
        {
            // Arrange
            var entity = new TestEntity();
            var domainEventMock = new Mock<IDomainEvent>();

            // Act
            entity.AddDomainEvent(domainEventMock.Object);
            entity.AddDomainEvent(domainEventMock.Object);

            // Assert
            entity.DomainEvents.Should().Contain(domainEventMock.Object).And.HaveCount(2);
        }

        [Fact]
        public void RemoveDomainEventThatWasNotAdded_ShouldNotChangeDomainEvents()
        {
            // Arrange
            var entity = new TestEntity();
            var domainEventMock = new Mock<IDomainEvent>();
            var anotherDomainEventMock = new Mock<IDomainEvent>();
            entity.AddDomainEvent(domainEventMock.Object);

            // Act
            entity.RemoveDomainEvent(anotherDomainEventMock.Object);

            // Assert
            entity.DomainEvents.Should().Contain(domainEventMock.Object);
            entity.DomainEvents.Should().NotContain(anotherDomainEventMock.Object);
        }

        [Fact]
        public void ClearDomainEvents_WhenNoEvents_ShouldRemainEmpty()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            entity.ClearDomainEvents();

            // Assert
            entity.DomainEvents.Should().BeEmpty();
        }

        [Fact]
        public void AddNullDomainEvent_ShouldThrowArgumentNullException()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            Action act = () => entity.AddDomainEvent(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*eventItem*");
        }

        [Fact]
        public void RemoveNullDomainEvent_ShouldThrowArgumentNullException()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            Action act = () => entity.RemoveDomainEvent(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*eventItem*");
        }
    }
}
