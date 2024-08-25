using MediatR;

namespace QingFa.EShop.Domain.Core.Events
{
    public interface IDomainEvent : INotification
    {
        public Guid Id { get; init; }
    }
}
