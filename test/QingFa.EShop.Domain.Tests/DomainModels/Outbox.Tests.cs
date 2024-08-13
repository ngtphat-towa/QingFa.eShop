using System.ComponentModel.DataAnnotations;

using Qingfa.EShop.Domain.DomainModels.Bases;

namespace Qingfa.EShop.Domain.Tests.DomainModels;
public class OutboxTests
{
    private class TestOutbox : Outbox
    {
        public TestOutbox(Guid id) : base(id) { }
    }

    [Fact]
    public void Validate_ShouldPass_WhenValidData()
    {
        var outbox = new TestOutbox(Guid.NewGuid())
        {
            Type = "Order",
            AggregateType = "OrderAggregate",
            AggregateId = Guid.NewGuid(),
            Payload = new byte[] { 1, 2, 3 }
        };

        bool isValid = outbox.Validate();

        Assert.True(isValid);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenTypeIsNull()
    {
        var outbox = new TestOutbox(Guid.NewGuid())
        {
            Type = null!,
            AggregateType = "OrderAggregate",
            AggregateId = Guid.NewGuid(),
            Payload = new byte[] { 1, 2, 3 }
        };

        Assert.Throws<ValidationException>(() => outbox.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenAggregateTypeIsNull()
    {
        var outbox = new TestOutbox(Guid.NewGuid())
        {
            Type = "Order",
            AggregateType = null!,
            AggregateId = Guid.NewGuid(),
            Payload = new byte[] { 1, 2, 3 }
        };

        Assert.Throws<ValidationException>(() => outbox.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenAggregateIdIsEmpty()
    {
        var outbox = new TestOutbox(Guid.NewGuid())
        {
            Type = "Order",
            AggregateType = "OrderAggregate",
            AggregateId = Guid.Empty,
            Payload = new byte[] { 1, 2, 3 }
        };

        Assert.Throws<ValidationException>(() => outbox.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenPayloadIsNullOrEmpty()
    {
        var outbox = new TestOutbox(Guid.NewGuid())
        {
            Type = "Order",
            AggregateType = "OrderAggregate",
            AggregateId = Guid.NewGuid(),
            Payload = Array.Empty<byte>()
        };

        Assert.Throws<ValidationException>(() => outbox.Validate());
    }
}
