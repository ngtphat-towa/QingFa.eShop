using System.Text.Json.Serialization;

using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    public abstract class AggregateRoot<TEntityId> :
        Entity<TEntityId>, IHasDomainEvent, IAggregateRoot<TEntityId> where TEntityId : notnull
    {
        [JsonIgnore]
        private readonly HashSet<IDomainEvent> _domainEvents= new();

        // Explicit interface implementation for IHasDomainEvent.DomainEvents
        IReadOnlyList<IDomainEvent> IHasDomainEvent.DomainEvents => _domainEvents.ToList();

        protected AggregateRoot()
        {
            _domainEvents = new HashSet<IDomainEvent>();
        }
        protected AggregateRoot(TEntityId id  =default!) :base(id)
        {
            _domainEvents = new HashSet<IDomainEvent>();
        }

        // Method to add a domain event
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        // Method to remove a domain event
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        void IHasDomainEvent.ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
