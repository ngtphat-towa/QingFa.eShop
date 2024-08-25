using MediatR;

using QingFa.EShop.Domain.Core.Events;

namespace QingFa.EShop.Application.Core.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
