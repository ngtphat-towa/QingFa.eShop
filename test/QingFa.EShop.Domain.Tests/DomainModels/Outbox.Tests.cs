using System.ComponentModel.DataAnnotations;

using FluentAssertions;

using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.eShop.Domain.Tests.DomainModels
{
    public class OutboxTests
    {
        private class TestOutbox : Outbox
        {
            public TestOutbox(Guid id) : base(id) { }
        }

        [Fact]
        public void Validate_ShouldPass_WhenValidData()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = "Order",
                AggregateType = "OrderAggregate",
                AggregateId = Guid.NewGuid(),
                Payload = new byte[] { 1, 2, 3 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().NotThrow();
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenTypeIsNull()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = null!,
                AggregateType = "OrderAggregate",
                AggregateId = Guid.NewGuid(),
                Payload = new byte[] { 1, 2, 3 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().Throw<ValidationException>()
                .WithMessage("Type of the Outbox entity couldn't be null or empty.");
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenAggregateTypeIsNull()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = "Order",
                AggregateType = null!,
                AggregateId = Guid.NewGuid(),
                Payload = new byte[] { 1, 2, 3 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().Throw<ValidationException>()
                .WithMessage("AggregateType of the Outbox entity couldn't be null or empty.");
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenAggregateIdIsEmpty()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = "Order",
                AggregateType = "OrderAggregate",
                AggregateId = Guid.Empty,
                Payload = new byte[] { 1, 2, 3 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().Throw<ValidationException>()
                .WithMessage("AggregateId of the Outbox entity couldn't be null.");
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenPayloadIsNullOrEmpty()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = "Order",
                AggregateType = "OrderAggregate",
                AggregateId = Guid.NewGuid(),
                Payload = Array.Empty<byte>()
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().Throw<ValidationException>()
                .WithMessage("Payload of the Outbox entity couldn't be null or empty (should be an Avro format).");
        }

        [Fact]
        public void Validate_ShouldPass_WhenPayloadIsNonEmpty()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = "Order",
                AggregateType = "OrderAggregate",
                AggregateId = Guid.NewGuid(),
                Payload = new byte[] { 1, 2, 3, 4, 5 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().NotThrow();
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenTypeIsEmpty()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = string.Empty,
                AggregateType = "OrderAggregate",
                AggregateId = Guid.NewGuid(),
                Payload = new byte[] { 1, 2, 3 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().Throw<ValidationException>()
                .WithMessage("Type of the Outbox entity couldn't be null or empty.");
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenAggregateTypeIsEmpty()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.NewGuid())
            {
                Type = "Order",
                AggregateType = string.Empty,
                AggregateId = Guid.NewGuid(),
                Payload = new byte[] { 1, 2, 3 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().Throw<ValidationException>()
                .WithMessage("AggregateType of the Outbox entity couldn't be null or empty.");
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenIdIsEmpty()
        {
            // Arrange
            var outbox = new TestOutbox(Guid.Empty)
            {
                Type = "Order",
                AggregateType = "OrderAggregate",
                AggregateId = Guid.NewGuid(),
                Payload = new byte[] { 1, 2, 3 }
            };

            // Act
            Action validateAction = () => outbox.Validate();

            // Assert
            validateAction.Should().Throw<ValidationException>()
                .WithMessage("Id of the Outbox entity couldn't be null.");
        }

    }
}
