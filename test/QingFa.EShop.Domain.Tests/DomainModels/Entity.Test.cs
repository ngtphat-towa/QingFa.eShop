using QingFa.EShop.Domain.DomainModels;
using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.eShop.Domain.Tests.DomainModels;
public class EntityTests
{
    private class TestEntity : Entity<int>
    {
        public TestEntity(int id) : base(id) { }

        public void Update(int userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }
    }

    private class TestDomainEvent : IDomainEvent
    {
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public string CorrelationId { get; init; } = Guid.NewGuid().ToString();
        public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();

        public void Flatten() { }
    }

    [Fact]
    public void AddDomainEvent_ShouldAddEvent()
    {
        var entity = new TestEntity(1);
        var domainEvent = new TestDomainEvent();

        entity.AddDomainEvent(domainEvent);

        Assert.Single(entity.DomainEvents);
        Assert.Contains(domainEvent, entity.DomainEvents);
    }

    [Fact]
    public void RemoveDomainEvent_ShouldRemoveEvent()
    {
        var entity = new TestEntity(1);
        var domainEvent = new TestDomainEvent();

        entity.AddDomainEvent(domainEvent);
        entity.RemoveDomainEvent(domainEvent);

        Assert.Empty(entity.DomainEvents);
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllEvents()
    {
        var entity = new TestEntity(1);
        entity.AddDomainEvent(new TestDomainEvent());
        entity.AddDomainEvent(new TestDomainEvent());

        entity.ClearDomainEvents();

        Assert.Empty(entity.DomainEvents);
    }

    [Fact]
    public void Entity_ShouldInitializeWithDefaults()
    {
        var entity = new TestEntity(1);

        Assert.Equal(1, entity.Id);
        Assert.InRange(entity.CreatedAt, DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
        Assert.Null(entity.UpdatedAt);
        Assert.Null(entity.UpdatedBy);
    }

    [Fact]
    public void Entity_Update_ShouldSetUpdatedProperties()
    {
        var entity = new TestEntity(1);
        int userId = 2;

        entity.Update(userId);

        Assert.InRange(entity.UpdatedAt!.Value, DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
        Assert.Equal(userId, entity.UpdatedBy);
    }

    [Fact]
    public void Entity_Equality_ShouldCompareById()
    {
        var entity1 = new TestEntity(1);
        var entity2 = new TestEntity(1);
        var entity3 = new TestEntity(2);

        Assert.Equal(entity1.Id, entity2.Id);
        Assert.NotEqual(entity1.Id, entity3.Id);
    }

    [Fact]
    public void DomainEvents_ShouldNotContainDuplicates()
    {
        var entity = new TestEntity(1);
        var domainEvent = new TestDomainEvent();

        entity.AddDomainEvent(domainEvent);
        entity.AddDomainEvent(domainEvent);

        Assert.Equal(2, entity.DomainEvents.Count);
        entity.ClearDomainEvents();
        Assert.Empty(entity.DomainEvents);
    }
}