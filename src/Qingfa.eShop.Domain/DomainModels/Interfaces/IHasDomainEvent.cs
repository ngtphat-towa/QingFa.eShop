namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    public interface IHasDomainEvent
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
        public void AddDomainEvent(IDomainEvent @event);
        public void ClearDomainEvents();
    }
}
