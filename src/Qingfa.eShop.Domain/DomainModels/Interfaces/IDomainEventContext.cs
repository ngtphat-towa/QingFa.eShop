namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    public interface IDomainEventContext
    {
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}
