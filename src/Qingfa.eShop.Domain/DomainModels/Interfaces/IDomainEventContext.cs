namespace QingFa.EShop.Domain.DomainModels
{
    public interface IDomainEventContext
    {
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}
