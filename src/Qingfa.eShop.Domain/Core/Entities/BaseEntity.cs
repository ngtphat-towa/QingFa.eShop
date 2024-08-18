using QingFa.EShop.Domain.Core.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace QingFa.EShop.Domain.Core.Entities
{
    public abstract class BaseEntity<IKey>
    {
        protected BaseEntity(IKey id) => Id = id;

        protected BaseEntity()
        {
            Id = default!;
        }

        public IKey Id { get; protected set; }

        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
